using System.Collections.Generic;
using Isu.Entities;
using IsuExtra.Entities;

namespace IsuExtra.Services
{
    public interface IIsuExtraServices
    {
        Ognp AddOgnp(string name, string nameMegaFaculty);
        MegaFaculty AddMegaFaculty(string nameMegaFaculty);
        Student AddStudentToMegaFaculty(string nameMegaFaculty, Student student);
        bool AddStudentToOgnp(Student student, string streamName);
        bool DeleteStudentToOgnp(Student student, string streamName);
        List<Stream> GetStreamsOnOgnp(string ognpName);
        List<Student> GetStudentsOnStream(string streamName);
        List<Student> GetFreeStudents(GroupName groupName);
        public Ognp FindOgnp(string name);
        public List<Student> GetStudentsOnMegaFaculty(string name);
    }
}