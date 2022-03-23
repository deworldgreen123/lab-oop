using Isu.Tools;

namespace Isu.Entities
{
    public class CourseNumber
    {
        private ushort _courseNumber;
        private ushort _maxNumberCourse = 4;
        private ushort _minNumberCourse = 1;

        public CourseNumber(ushort courseNumber)
        {
            if (courseNumber > _maxNumberCourse || courseNumber < _minNumberCourse)
            {
                throw new IsuException("YOUR_ERROR: Incorrectly CourseNumber");
            }

            _courseNumber = courseNumber;
        }

        public ushort NumberOfCourse
        {
            get
            {
                return _courseNumber;
            }
        }

        public ushort GetCourseNumber()
        {
            return _courseNumber;
        }
    }
}