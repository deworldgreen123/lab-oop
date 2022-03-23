using System;
using Isu.Tools;

namespace Isu.Entities
{
    public class GroupName
    {
        private string _specialization;
        private CourseNumber _courseNumber;
        private short _groupNumber;
        private short _lenghtGroupName = 5;

        public GroupName()
        {
            _specialization = null;
        }

        public GroupName(string name)
        {
            if (name.Length != _lenghtGroupName)
            {
                throw new IsuException("YOUR_ERROR: string length is not 5");
            }

            if (!char.IsLetter(name[0]) || !char.IsDigit(name[1]))
            {
                throw new IsuException("YOUR_ERROR: Incorrectly Specialization");
            }

            if (!char.IsDigit(name[2]))
            {
                throw new IsuException("YOUR_ERROR: Incorrectly CourseNumber");
            }

            if (!char.IsDigit(name[3]) || !char.IsDigit(name[4]))
            {
                throw new IsuException("YOUR_ERROR: Incorrectly GroupNumber");
            }

            _specialization = name.Substring(0, 2);
            _courseNumber = new CourseNumber(Convert.ToUInt16(name.Substring(2, 1)));
            _groupNumber = Convert.ToInt16(name.Substring(3, 2));
        }

        public string GetName()
        {
            if (_groupNumber < 10)
            {
                return _specialization + Convert.ToString(_courseNumber.NumberOfCourse) + "0" +
                       Convert.ToString(_groupNumber);
            }
            else
            {
                return _specialization + Convert.ToString(_courseNumber.NumberOfCourse) +
                       Convert.ToString(_groupNumber);
            }
        }

        public string GetSpecialization()
        {
            return _specialization;
        }

        public CourseNumber GetCourseNumber()
        {
            return _courseNumber;
        }

        public short GetGroupNumber()
        {
            return _groupNumber;
        }
    }
}