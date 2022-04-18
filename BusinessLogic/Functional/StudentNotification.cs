using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Functional
{
    public class StudentNotification : IStudentNotificationService
    {
        private readonly ILectionLogRepository _lectionLogRepository;
        private readonly ICrudRepository<Lection> _lectionRepository;
        
        private readonly ILogger<StudentNotification> _logger;
        
        public StudentNotification(ILectionLogRepository lectionLogRepository, ICrudRepository<Lection> lectionRepository, ILogger<StudentNotification> logger)
        {
            _lectionLogRepository = lectionLogRepository;
            _lectionRepository = lectionRepository;
            _logger = logger;
        }

        public void Check(Student student, Lector lector)
        {
            _logger.LogInformation("Start method \"Check\"");
            
            
            // Find if added student have more than 3 passes to send email to student and lector
            var logs = _lectionLogRepository.GetAll().ToArray();
            IEnumerable<LectionLog> logsForPasses = logs.Where(c => c.StudentId == student.Id);
            int lectionPass = 0;
            foreach (var log in logsForPasses)
            {
                if (!log.Attendance) lectionPass++;
                if (lectionPass > 3)
                {
                    SendNotificationViaEmail(student.Email, "Why are you passed more than 3 lectures");
                    SendNotificationViaEmail(lector.Email, $"Student {student.Name} have passed more than 3 lectures");
                }
            }


            // Find if added student have average score less than 4 to send him sms
            var lections = _lectionRepository.GetAll().ToArray();

            var lectorLections = lections.Where(c => c.LectorId == lector.Id);
            
            var logsLector = logs.Where(c => lectorLections.Any(cc => cc.Id == c.LectionId));

            var studentsAverageScores = from log in logsLector
                group log by log.StudentId into playerGroup
                select new
                {
                    StudentId = playerGroup.Key,
                    AverageScore = playerGroup.Average(x => x.Score),
                };
            var res = studentsAverageScores.Where(c => c.StudentId == student.Id);
            foreach (var r in res)
            {
                if (r.AverageScore < 4)
                    SendNotificationViaSms(student.PhoneNumber, "Why average score is less than 4");
            }
        }

        private void SendNotificationViaEmail(string email, string message)
        {
            _logger.LogInformation($"Send message to email: {email}");
            // send message to email
        }

        private void SendNotificationViaSms(string phoneNumber, string message)
        {
            _logger.LogInformation($"Send sms to phone number: {phoneNumber}");
            // send sms to phoneNumber
        }
    }
}