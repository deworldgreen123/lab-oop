using System;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;
using Isu.Entities;

namespace Isu.Tests
{
    public class Tests
    {
        private IsuService _isuService;

        [SetUp]
        public void Setup()
        {
            _isuService = new Entities.Isu();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group group = _isuService.AddGroup(new GroupName("M3103"));
            Student student = _isuService.AddStudent(group, "Enkeev Bair");
            Assert.AreEqual(_isuService.GetStudent(student.GetId()), student);
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup(new GroupName("M3203"));
                for (int i = 0; i < 32; i++)
                {
                    _isuService.AddStudent(group, Convert.ToString(i));
                }
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group1 = _isuService.AddGroup(new GroupName("MM305"));

            });
            Assert.Catch<IsuException>(() =>
            {
                Group group2 = _isuService.AddGroup(new GroupName("M305"));
            });
            Assert.Catch<IsuException>(() =>
            {
                Group group3 = _isuService.AddGroup(new GroupName("M3005"));
            });
            Assert.Catch<IsuException>(() =>
            {
                Group group4 = _isuService.AddGroup(new GroupName("M3705"));
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group oldGroup = _isuService.AddGroup(new GroupName("M3203"));
            Student student = _isuService.AddStudent(oldGroup, "Bair");
            Assert.AreEqual(oldGroup.GetGroupName(), student.GetGroupName());
            Group newGroup = _isuService.AddGroup(new GroupName("M3201"));
            _isuService.ChangeStudentGroup(student, newGroup);
            Assert.AreEqual(newGroup.GetGroupName(), student.GetGroupName());
        }
    }
}