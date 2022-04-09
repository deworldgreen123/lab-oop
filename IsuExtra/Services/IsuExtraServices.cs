using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Services;
using IsuExtra.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class IsuExtraServices : IIsuExtraServices
    {
        private List<MegaFaculty> _megaFaculties = new List<MegaFaculty>();
        private IsuService _isuService;
        private List<Ognp> _ognps = new List<Ognp>();

        public IsuExtraServices(IsuService isuService)
        {
            _isuService = isuService;
        }

        public Ognp AddOgnp(string name, string nameMegaFaculty)
        {
            var newOgnp = new Ognp(name, nameMegaFaculty);
            if (_ognps.Contains(newOgnp))
            {
                return null;
            }

            _ognps.Add(newOgnp);
            return newOgnp;
        }

        public MegaFaculty AddMegaFaculty(string nameMegaFaculty)
        {
            var newMegaFaculty = new MegaFaculty(nameMegaFaculty);
            if (_megaFaculties.Contains(newMegaFaculty))
            {
                return null;
            }

            _megaFaculties.Add(newMegaFaculty);
            return newMegaFaculty;
        }

        public Student AddStudentToMegaFaculty(string nameMegaFaculty, Student student)
        {
            foreach (MegaFaculty megaFaculty in _megaFaculties.Where(megaFaculty => megaFaculty.GetName() == nameMegaFaculty))
            {
                return megaFaculty.AddStudent(student);
            }

            return null;
        }

        public bool AddStudentToOgnp(Student student, string streamName)
        {
            foreach (Ognp ognp in _ognps.Where(ognp => ognp.FindStream(streamName) != null))
            {
                if (GetStudentsOnMegaFaculty(ognp.GetNameMegaFaculty()) != null)
                {
                    if (GetStudentsOnMegaFaculty(ognp.GetNameMegaFaculty()).Contains(student))
                        throw new IsuExtraException("YOUR_ERROR");
                }

                Stream stream = ognp.FindStream(streamName);

                if (GetStudentMegaFaculty(student) != null)
                {
                    TimeTable timeTableStudent = GetStudentMegaFaculty(student).GetTimeTableToGroup(student.GetGroupName());
                    if (timeTableStudent.InteractionTime(stream.GetLessons()))
                    {
                        throw new IsuExtraException("YOUR_ERROR");
                    }
                }

                stream.AddStudent(student);
                return true;
            }

            return false;
        }

        public bool DeleteStudentToOgnp(Student student, string streamName)
        {
            foreach (var ognp in _ognps.Where(ognp => ognp.FindStream(streamName) != null))
            {
                ognp.DeleteStudent(student, ognp.FindStream(streamName));
                return true;
            }

            return false;
        }

        public List<Stream> GetStreamsOnOgnp(string ognpName)
        {
            return (from ognp in _ognps where ognp.GetName() == ognpName select ognp.GetStreams()).FirstOrDefault();
        }

        public List<Student> GetStudentsOnStream(string streamName)
        {
            foreach (Ognp ognp in _ognps.Where(ognp => ognp.FindStream(streamName) != null))
            {
                return ognp.FindStream(streamName).GetStudents();
            }

            return null;
        }

        public List<Student> GetFreeStudents(GroupName groupName)
        {
            List<Student> students = _isuService.FindStudents(groupName);
            foreach (Student student in students)
            {
                foreach (Ognp ognp in _ognps.Where(ognp => ognp.CheckStudent(student)))
                {
                    students.Remove(student);
                }
            }

            return students;
        }

        public Ognp FindOgnp(string name)
        {
            return _ognps.FirstOrDefault(ognp => ognp.GetName() == name);
        }

        public List<Student> GetStudentsOnMegaFaculty(string name)
        {
            return _megaFaculties.FirstOrDefault(megaFaculty => megaFaculty.GetName() == name)?.GetStudents();
        }

        public MegaFaculty GetStudentMegaFaculty(Student student)
        {
            return _megaFaculties.FirstOrDefault(megaFaculty => megaFaculty.GetStudents().Contains(student));
        }
    }
}