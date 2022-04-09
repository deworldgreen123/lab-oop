using System;
using System.Collections.Generic;
using Isu.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class Stream
    {
        private TimeTable _lessons;
        private List<Student> _students = new List<Student>();
        private string _name;
        private int _maxNumberStudents;
        private int _currentNumberStudents = 0;

        public Stream(TimeTable lessons, string name, int maxStudents)
        {
            _lessons = lessons;
            _name = name;
            _maxNumberStudents = maxStudents;
        }

        public Student AddStudent(Student newStudent)
        {
            if (_currentNumberStudents == _maxNumberStudents)
            {
                throw new IsuExtraException("YOUR_ERROR");
            }

            _students.Add(newStudent);
            _currentNumberStudents++;
            return newStudent;
        }

        public bool DeleteStudent(Student student)
        {
            if (!_students.Contains(student))
            {
                return false;
            }

            _students.Remove(student);
            _currentNumberStudents--;
            return true;
        }

        public bool AddLesson(Lesson lesson, int day)
        {
            if (_lessons.FreeLesson(lesson.GetLessonNumber(), day))
            {
                return false;
            }

            _lessons.AddLesson(lesson, day);
            return true;
        }

        public bool IsStudent(Student student)
        {
            return _students.Contains(student);
        }

        public TimeTable GetLessons()
        {
            return _lessons;
        }

        public string GetName()
        {
            return _name;
        }

        public int GetCount()
        {
            return _currentNumberStudents;
        }

        public List<Student> GetStudents()
        {
            return _students;
        }
    }
}