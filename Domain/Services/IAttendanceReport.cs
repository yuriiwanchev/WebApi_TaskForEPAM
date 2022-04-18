using Domain.Models;

namespace Domain.Services
{
    public interface IAttendanceReport
    {
        void GenerateAttendanceStudentReport(string studentName, ISerializator serializator);
        void GenerateAttendanceLectionReport(string lectionName, ISerializator serializator);
    }
}