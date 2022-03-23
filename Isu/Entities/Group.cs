using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        private GroupName _groupName;
        private List<Student> _students;
        private ushort _maxCountOfStudents = 30;
        public Group()
        {
            _groupName = new GroupName();
            _students = new List<Student>();
        }

        public Group(GroupName groupName)
        {
            _groupName = groupName;
            _students = new List<Student>();
        }

        public Group(GroupName groupName, List<Student> students)
        {
            _groupName = groupName;
            if (students.Count > _maxCountOfStudents)
                throw new IsuException("YOUR_ERROR: Exceeding the count of students");
            _students = students;
        }

        public void AddStudent(Student student)
        {
            if (_students.Count == _maxCountOfStudents)
                throw new IsuException("YOUR_ERROR: Exceeding the count of students");
            _students.Add(student);
            if (student.GetGroupName() == null)
                throw new IsuException("YOUR_ERROR: The student does not have a group");
            student.SetGroupName(_groupName);
        }

        public GroupName GetGroupName()
        {
            return _groupName;
        }

        internal List<Student> GetStudents()
        {
            return _students;
        }

        internal Student FindStudent(int id)
        {
            foreach (Student student in _students)
            {
                if (student.GetId() == id)
                    return student;
            }

            return null;
        }

        internal Student FindStudent(string name)
        {
            foreach (Student student in _students)
            {
                if (student.GetName() == name)
                    return student;
            }

            return null;
        }

        internal void DelateStudent(Student studentToDelete)
        {
            if (studentToDelete == null)
                throw new IsuException("YOUR_ERROR: student is null");
            _students.Remove(studentToDelete);
        }
    }
}