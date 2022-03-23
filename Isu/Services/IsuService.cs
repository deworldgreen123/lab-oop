using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Isu.Entities;

namespace Isu.Services
{
    public interface IsuService
    {
        Group AddGroup(GroupName name);
        Student AddStudent(Group group, string name);

        Student GetStudent(int id);
        Student FindStudent(string name);
        List<Student> FindStudents(GroupName groupName);
        List<Student> FindStudents(CourseNumber courseNumber);

        Group FindGroup(GroupName groupName);
        List<Group> FindGroups(CourseNumber courseNumber);

        void ChangeStudentGroup(Student student, Group newGroup);
    }
}