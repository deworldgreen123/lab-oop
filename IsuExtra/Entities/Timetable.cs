using System.Collections.Generic;
using Isu.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class TimeTable
    {
        private const int NumberOfStudyDay = 12;
        private List<TimeTableDay> _timeTableWeek;

        public TimeTable()
        {
            _timeTableWeek = new List<TimeTableDay>();
            for (int i = 0; i < NumberOfStudyDay; i++)
            {
                _timeTableWeek.Add(new TimeTableDay());
            }
        }

        public bool AddLesson(Lesson lesson, int day)
        {
            if (day < 0 && day > NumberOfStudyDay)
            {
                throw new IsuExtraException("YOUR_ERROR");
            }

            return _timeTableWeek[day].AddLesson(lesson);
        }

        public bool AddLesson(int lessonNumber, GroupName groupName, int day)
        {
            return _timeTableWeek[day].AddLesson(lessonNumber, groupName);
        }

        public bool RemoveLesson(int lessonNumber, int day)
        {
            return _timeTableWeek[day].RemoveLesson(lessonNumber);
        }

        public bool FreeLesson(int lessonNumber, int day)
        {
            return _timeTableWeek[day].FreeLesson(lessonNumber);
        }

        public bool InteractionTime(TimeTable anotherTimeTable)
        {
            for (int dayNumber = 0; dayNumber < NumberOfStudyDay; dayNumber++)
            {
                if (!_timeTableWeek[dayNumber].InteractionTime(anotherTimeTable._timeTableWeek[dayNumber]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}