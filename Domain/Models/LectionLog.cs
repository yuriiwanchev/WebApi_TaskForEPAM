namespace Domain.Models
{
    public record LectionLog(int LectionId, int StudentId, bool Attendance, int HomeworkId, int Score);
}