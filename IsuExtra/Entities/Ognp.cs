using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class Ognp
    {
        private List<Stream> _streams = new List<Stream>();
        private string _name;
        private string _nameMegaFaculty;

        public Ognp(string name, string nameMegaFaculty)
        {
            _name = name;
            _nameMegaFaculty = nameMegaFaculty;
        }

        public Stream AddStream(TimeTable timeTable, string nameStream, int maxStudentToStream)
        {
            var newStream = new Stream(timeTable, nameStream, maxStudentToStream);
            if (_streams.Contains(newStream))
            {
                return null;
            }

            _streams.Add(newStream);
            return newStream;
        }

        public Stream FindStream(string nameStream)
        {
            return _streams.FirstOrDefault(stream => stream.GetName() == nameStream);
        }

        public bool AddStudent(Student newStudent, Stream stream)
        {
            if (stream.GetStudents().Contains(newStudent))
            {
                return false;
            }

            stream.AddStudent(newStudent);
            return true;
        }

        public void AddStudent(Student newStudent)
        {
            foreach (Stream stream in _streams)
            {
                stream.AddStudent(newStudent);
            }
        }

        public bool DeleteStudent(Student newStudent, Stream stream)
        {
            if (!stream.GetStudents().Contains(newStudent))
            {
                return false;
            }

            stream.DeleteStudent(newStudent);
            return true;
        }

        public void DeleteStudent(Student student)
        {
            foreach (Stream stream in _streams)
            {
                stream.DeleteStudent(student);
            }
        }

        public bool CheckStudent(Student student)
        {
            return _streams.Any(stream => stream.GetStudents().Contains(student));
        }

        public string GetName()
        {
            return _name;
        }

        public string GetNameMegaFaculty()
        {
            return _nameMegaFaculty;
        }

        public List<Stream> GetStreams()
        {
            return _streams;
        }
    }
}