using System.Collections.Generic;
using System.Linq;
using Isu.Services;
using Isu.Tools;

namespace Isu.Entities
{
    public class Isu : IsuService
    {
        private int _nextId;
        private List<Group> _groups;

        public Isu()
        {
            _groups = new List<Group>();
            _nextId = 1;
        }

        public Group AddGroup(GroupName name)
        {
            _groups.Add(new Group(name));
            return _groups.Last();
        }

        public Student AddStudent(Group group, string name)
        {
            var student = new Student(_nextId++, name, group.GetGroupName());
            if (FindGroup(group.GetGroupName()) == null)
                throw new IsuException("YOUR_ERROR: Absence of a group");

            group.AddStudent(student);
            return student;
        }

        public Student GetStudent(int id)
        {
            foreach (Group group in _groups)
            {
                Student student = group.FindStudent(id);
                if (student != null)
                {
                    return student;
                }
            }

            throw new IsuException("YOUR_ERROR: there is no student with this id");
        }

        public Student FindStudent(string name)
        {
            foreach (Group group in _groups)
            {
                Student student = group.FindStudent(name);
                if (student != null)
                {
                    return student;
                }
            }

            return null;
        }

        public List<Student> FindStudents(GroupName groupName)
        {
            foreach (Group group in _groups)
            {
                if (group.GetGroupName().GetName() == groupName.GetName())
                    return group.GetStudents();
            }

            return null;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var students = new List<Student>();
            foreach (Group group in _groups)
            {
                if (group.GetGroupName().GetCourseNumber().NumberOfCourse == courseNumber.NumberOfCourse)
                {
                    AddStudents(students, @group.GetStudents());
                }
            }

            void AddStudents(List<Student> studentsOne, List<Student> studentsTwo)
            {
                foreach (Student student in studentsTwo)
                {
                    studentsOne.Add(student);
                }
            }

            return students;
        }

        public Group FindGroup(GroupName groupName)
        {
            foreach (Group group in _groups)
            {
                if (group.GetGroupName().GetName() == groupName.GetName())
                {
                    return group;
                }
            }

            return null;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            var groups = new List<Group>();
            foreach (Group group in _groups)
            {
                if (group.GetGroupName().GetCourseNumber() == courseNumber)
                {
                    groups.Add(group);
                }
            }

            return groups;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (FindGroup(newGroup.GetGroupName()) == null)
                AddGroup(newGroup.GetGroupName());
            Group oldGroup = FindGroup(student.GetGroupName());
            if (oldGroup == null)
                throw new IsuException("YOUR_ERROR: Absence of a group");
            oldGroup.DelateStudent(student);
            FindGroup(newGroup.GetGroupName()).AddStudent(student);
        }
    }
}