using System;
using IsuExtra.Services;
using IsuExtra.Entities;
using IsuExtra.Tools;
using Isu.Entities;
using Isu.Services;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class IsuExtraTest
    {
        private IsuService _isuService;
        private IIsuExtraServices _isuExtraService;

        [SetUp]
        public void Setup()
        {
            _isuService = new Isu.Entities.Isu();
            _isuExtraService = new IsuExtraServices(_isuService);
        }

        [Test]
        public void CheckNewOgnp()
        {
            const string nameNewOgnp = "BJD";
            Ognp newOgnp = _isuExtraService.AddOgnp(nameNewOgnp, "TINT");
            Assert.AreEqual(_isuExtraService.FindOgnp(nameNewOgnp), newOgnp);
        }

        [Test]
        public void AddAndDeleteStudentToStream()
        {
            var groupName = new GroupName("M3203");
            Group group = _isuService.AddGroup(groupName);
            Student student = _isuService.AddStudent(group, "Bair");
            
            const string nameOgnp = "BJD";
            Ognp ognp = _isuExtraService.AddOgnp(nameOgnp, "TINT");
            const string nameStream = "BJD1.4";
            Stream stream = ognp.AddStream(new TimeTable(), nameStream, 30);

            _isuExtraService.AddMegaFaculty("TINT");
            _isuExtraService.AddStudentToOgnp(student, nameStream);
            Assert.AreEqual(_isuExtraService.GetStudentsOnStream(nameStream)[0], student);
            _isuExtraService.DeleteStudentToOgnp(student, nameStream);
            Assert.IsFalse(stream.IsStudent(student));
        }

        [Test]
        public void ReachMaxStudentPerStream_ThrowException()
        {
            Assert.Catch<IsuExtraException>(() =>
            {
                Group group = _isuService.AddGroup(new GroupName("M3203"));
                Ognp ognp = _isuExtraService.AddOgnp("BJD", "TINT");
                Stream stream = ognp.AddStream(new TimeTable(), "BJD1.4", 5);
                _isuExtraService.AddMegaFaculty("TINT");
                
                for (int i = 0; i < 32; i++)
                {
                    Student newStudent = _isuService.AddStudent(group, Convert.ToString(i));
                    _isuExtraService.AddStudentToOgnp(newStudent, "BJD1.4");
                }
            });
        }

        [Test]
        public void IntersectionOfLessons_ThrowException()
        {
            Assert.Catch<IsuExtraException>(() =>
            {
                var groupName = new GroupName("M3203");
                Group group = _isuService.AddGroup(groupName);
                Student student = _isuService.AddStudent(group, "Bair");

                Ognp ognp = _isuExtraService.AddOgnp("BJD", "TINT");
                
                MegaFaculty megaFaculty = _isuExtraService.AddMegaFaculty("TINT1");
                _isuExtraService.AddStudentToMegaFaculty("TINT1", student);

                var lesson1 = new Lesson(1, groupName);
                var lesson2 = new Lesson(1, new GroupName("M3202"));

                megaFaculty.AddLesson(lesson1, 1);
                Stream stream = ognp.AddStream(new TimeTable(), "BJD1.4", 30);
                stream.AddLesson(lesson2, 1);
                
                _isuExtraService.AddStudentToOgnp(student, "BJD1.4");
            });
        }
    }
}