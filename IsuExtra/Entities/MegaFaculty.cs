using System.Collections.Generic;
using System.Diagnostics;
using Isu.Entities;

namespace IsuExtra.Entities
{
    public class MegaFaculty
    {
        private string _name;
        private List<Student> _students = new List<Student>();
        private Dictionary<string, TimeTable> _timeTableOfGroup = new Dictionary<string, TimeTable>();

        public MegaFaculty(string name)
        {
            _name = name;
        }

        public Student AddStudent(Student student)
        {
            if (_students.Contains(student))
            {
                return null;
            }

            _students.Add(student);
            return student;
        }

        public bool AddLesson(Lesson lesson, int day)
        {
            if (!_timeTableOfGroup.ContainsKey(lesson.GetGroupName().GetName()))
            {
                _timeTableOfGroup[lesson.GetGroupName().GetName()] = new TimeTable();
            }

            return _timeTableOfGroup[lesson.GetGroupName().GetName()].AddLesson(lesson, day);
        }

        public string GetName()
        {
            return _name;
        }

        public List<Student> GetStudents()
        {
            return _students;
        }

        public TimeTable GetTimeTableToGroup(GroupName groupName)
        {
            return _timeTableOfGroup[groupName.GetName()];
        }
    }
}