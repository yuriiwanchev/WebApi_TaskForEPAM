using System.Collections;
using System.Collections.Generic;
using BusinessLogic.Functional;
using Domain.Helpers;
using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RestApi.Serializers;

namespace BusinessLogic.Tests
{
    [TestFixture]
    public class AttendanceReportTests
    {

        [Test]
        public void GenerateAttendanceStudentReport_StudentNameNull_Throw()
        {
            var studentServiceMock = new Mock<ICrudRepository<Student>>();
            var lectionService = new Mock<ICrudRepository<Lection>>();
            var lectionLogService = new Mock<ILectionLogRepository>();
                
            var logger = new Mock<ILogger<AttendanceReport>>();


            studentServiceMock.Setup(r => r.GetAll()).Returns(new List<Student>());
            
            var jsonSerializator = new JsonSerializator();
            
            var ff = new AttendanceReport(lectionLogService.Object, lectionService.Object, studentServiceMock.Object, logger.Object);
            
            Assert.Throws<InvalidUserInputException>(delegate { ff.GenerateAttendanceLectionReport("Creating types in C#", jsonSerializator); });
        }
    }
}

