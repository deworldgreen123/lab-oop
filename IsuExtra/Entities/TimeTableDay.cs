using System.Collections.Generic;
using Isu.Entities;

namespace IsuExtra.Entities
{
    public class TimeTableDay
    {
        private readonly List<Lesson> _lessons;
        private int _maxLessonsNumber = 8;

        public TimeTableDay(List<Lesson> lessons)
        {
            _lessons = lessons;
        }

        public TimeTableDay()
        {
            _lessons = new List<Lesson>();
            for (int i = 0; i < _maxLessonsNumber; i++)
            {
                _lessons.Add(new Lesson());
            }
        }

        public bool AddLesson(Lesson lesson)
        {
            if (_lessons[lesson.GetLessonNumber()] == null)
            {
                return false;
            }

            _lessons[lesson.GetLessonNumber()] = lesson;
            return true;
        }

        public bool AddLesson(int lessonNumber, GroupName groupName)
        {
            if (_lessons[lessonNumber] == null)
            {
                return false;
            }

            _lessons[lessonNumber] = new Lesson(lessonNumber, groupName);
            return true;
        }

        public bool RemoveLesson(int lessonNumber)
        {
            if (_lessons[lessonNumber] == null)
            {
                return false;
            }

            _lessons[lessonNumber] = null;
            return true;
        }

        public bool FreeLesson(int lessonNumber)
        {
            return _lessons[lessonNumber] == null;
        }

        public bool InteractionTime(TimeTableDay anotherTimeTableDay)
        {
            for (int lessonNumber = 0; lessonNumber < _maxLessonsNumber; lessonNumber++)
            {
                if (FreeLesson(lessonNumber) && anotherTimeTableDay.FreeLesson(lessonNumber))
                {
                    return false;
                }
            }

            return true;
        }
    }
}