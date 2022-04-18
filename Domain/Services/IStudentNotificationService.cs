using Domain.Models;

namespace Domain.Services
{
    public interface IStudentNotificationService
    {
        void Check(Student student, Lector lector);
    }
}