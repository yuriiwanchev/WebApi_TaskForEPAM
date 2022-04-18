using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Domain;
using Domain.Helpers;
using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Functional
{
    public class AttendanceReport : IAttendanceReport
    {
        private readonly ILectionLogRepository _lectionLogRepository;
        private readonly ICrudRepository<Lection> _lectionRepository;
        private readonly ICrudRepository<Student> _studentRepository;
        
        private readonly ILogger<AttendanceReport> _logger;
        
        private readonly string _path = "./Reports/";
        
        public AttendanceReport(
            ILectionLogRepository lectionLogRepository, 
            ICrudRepository<Lection> lectionRepository, 
            ICrudRepository<Student> studentRepository, 
            ILogger<AttendanceReport> logger)
        {
            _lectionRepository = lectionRepository;
            _lectionLogRepository = lectionLogRepository;
            _studentRepository = studentRepository;
            _logger = logger;
        }
        
        private void WriteToFile(string toWrite, string fileName)
        {
            File.WriteAllText(_path + fileName, toWrite);
        }
        
        public void GenerateAttendanceStudentReport(string studentName, ISerializator serializator)
        {
            _logger.LogInformation($"Start to generate report for student attendance: {studentName}");
            
            var students = _studentRepository.GetAll().ToArray();
            var studentsWithName = students.Where(c => c.Name == studentName);
            Student student = new Student(0,String.Empty, String.Empty, String.Empty);
            try
            {
                student = studentsWithName.First();
            }
            catch(System.InvalidOperationException e)
            {
                _logger.LogError($"There is no student \"{studentName}\" for generation report.");
                throw new InvalidUserInputException($"There is no student \"{studentName}\" for generation report.");
            }

            var lections = _lectionRepository.GetAll().ToArray();
            var logs = _lectionLogRepository.GetAll().ToArray();
            
            var asset = new StudentAttendanceReportAsset();
            var attendanceList = new List<LectionAttendance>();
            foreach (var log in logs)
            {
                if ( log.StudentId != student.Id) continue;
                var lection = lections.Where(c => c.Id == log.LectionId);
            
                var l = new LectionAttendance()
                {
                    LectionName = lection.First().Name,
                    Attendance = log.Attendance
                };
                attendanceList.Add(l);
            }
            asset.lections = attendanceList.ToArray();
            
            var report = serializator.SerializeObject(asset);
            
            string date = DateTime.Now.DayOfYear + "_" + DateTime.Now.Hour + "_" 
                          + DateTime.Now.Minute + "_" + DateTime.Now.Second + "_" + DateTime.Now.Millisecond;

            WriteToFile(report, "LectionReport_" + date + serializator.SerializationFormat);
        }

        public void GenerateAttendanceLectionReport(string lectionName, ISerializator serializator)
        {
            _logger.LogInformation($"Start to generate report for lection attendance: {lectionName}");
            
            var lections = _lectionRepository.GetAll().ToArray();
            var lectionsWithName = lections.Where(c => c.Name == lectionName);
            Lection lection = new Lection(0,"",0,DateTime.MinValue);
            try
            {
                lection = lectionsWithName.First();
            }
            catch(System.InvalidOperationException e)
            {
                _logger.LogError($"There is no lection \"{lectionName}\" for generation report.");
                throw new InvalidUserInputException($"There is no lection \"{lectionName}\" for generation report.");
            }

            var students = _studentRepository.GetAll().ToArray();
            var logs = _lectionLogRepository.GetAll().ToArray();

            var asset = new LectionAttendanceReportAsset();
            var studentAttendanceList = new List<StudentAttendance>();
            foreach (var log in logs)
            {
                if ( log.LectionId != lection.Id) continue;
                var student = students.Where(c => c.Id == log.StudentId);
            
                var st = new StudentAttendance()
                {
                    StudentName = student.First().Name,
                    Attendance = log.Attendance
                };
                studentAttendanceList.Add(st);
            }
            asset.students = studentAttendanceList.ToArray();

            var report = serializator.SerializeObject(asset);

            string date = DateTime.Now.DayOfYear + "_" + DateTime.Now.Hour + "_" 
                          + DateTime.Now.Minute + "_" + DateTime.Now.Second + "_" + DateTime.Now.Millisecond;

            WriteToFile(report, "LectionReport_" + date + serializator.SerializationFormat);
        }
    }
    
    
    
    // For student report
    [DataContract]
    internal class StudentAttendanceReportAsset
    {
        [DataMember]
        public LectionAttendance[] lections { get; set; } = { };
    }

    [DataContract]
    internal class LectionAttendance
    {
        [DataMember]
        public string LectionName { get; set; }
        [DataMember]
        public bool Attendance { get; set; }
    }
    
    // For lecture report
    [DataContract]
    internal class LectionAttendanceReportAsset
    {
        [DataMember]
        public StudentAttendance[] students { get; set; } = { };
    }

    [DataContract]
    internal class StudentAttendance
    {
        [DataMember]
        public string StudentName { get; set; }
        [DataMember]
        public bool Attendance { get; set; }
    }
    
    //--------------------------------------------------------------------------------------

    
}