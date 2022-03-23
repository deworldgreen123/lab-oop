using System;
using Isu.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class Lesson
    {
        private const int DurationLesson = 90;
        private readonly int _lessonNumber;
        private int _hours;
        private int _minutes;
        private GroupName _groupName;

        public Lesson()
        {
            _lessonNumber = -1;
        }

        public Lesson(int lessonNumber, GroupName groupName)
        {
            switch (lessonNumber)
            {
                case 1:
                    SetTime(8, 20);
                    break;
                case 2:
                    SetTime(10, 00);
                    break;
                case 3:
                    SetTime(11, 40);
                    break;
                case 4:
                    SetTime(13, 30);
                    break;
                case 5:
                    SetTime(15, 20);
                    break;
                case 6:
                    SetTime(17, 00);
                    break;
                case 7:
                    SetTime(18, 40);
                    break;
                case 8:
                    SetTime(20, 20);
                    break;
                default:
                    throw new IsuExtraException("YOUR_ERROR: Incorrect lesson number");
            }

            _lessonNumber = lessonNumber;
            _groupName = groupName;
        }

        public GroupName GetGroupName()
        {
            return _groupName;
        }

        public int GetHours()
        {
            return _hours;
        }

        public int GetMinutes()
        {
            return _minutes;
        }

        public int GetTime()
        {
            return (_hours * 60) + _minutes;
        }

        public int GetDuration()
        {
            return DurationLesson;
        }

        public int GetLessonNumber()
        {
            return _lessonNumber;
        }

        private void SetTime(int hours, int minutes)
        {
            _hours = hours;
            _minutes = minutes;
        }
    }
}