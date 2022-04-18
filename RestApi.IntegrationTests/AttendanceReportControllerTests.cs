using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace RestApi.IntegrationTests
{
    [TestFixture]
    public class AttendanceReportControllerTests
    {
        private HttpClient _httpClient;
        
        [OneTimeSetUp]
        public void Setup()
        {
            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        var dbService = 
                            services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DataAccess.StudentsDbContext>));
                        
                        services.Remove(dbService);

                        services.AddDbContext<DataAccess.StudentsDbContext>(options =>
                            options.UseInMemoryDatabase("test")
                        );
                    });
                });

            DataAccess.StudentsDbContext dbContext = webHost.Services.CreateScope().ServiceProvider
                .GetService<DataAccess.StudentsDbContext>();

            List<DataAccess.Models.StudentDb> students = new() {
                new DataAccess.Models.StudentDb{Id = 1, Name = "Ds", Email = "sd@ya.ru", PhoneNumber = "+7 924 257-93-12"}, 
                new DataAccess.Models.StudentDb{Id = 2, Name="Alice Film",Email = "aliceF@mail.com", PhoneNumber = "8 912 257-93-12"}
            };
            
            List<DataAccess.Models.LectorDb> lectors = new() {
                new DataAccess.Models.LectorDb{Id = 1, Name = "Ds", Email = "sd@ya.ru"}, 
                new DataAccess.Models.LectorDb{Id = 2, Name="Alice Film",Email = "aliceF@mail.com"}
            };
            
            List<DataAccess.Models.LectionDb> lections = new() {
                new DataAccess.Models.LectionDb{Id = 1, Name = "Ds", LectorId = 2, Date = DateTime.Now}, 
                new DataAccess.Models.LectionDb{Id = 2, Name="Alice Film", LectorId = 1, Date = DateTime.Now}
            };
            
            List<DataAccess.Models.HomeworkDb> homework = new() {
                new DataAccess.Models.HomeworkDb{Id = 1, Name = "Ds"}, 
                new DataAccess.Models.HomeworkDb{Id = 2, Name="Alice Film"}
            };
            
            List<DataAccess.Models.LectionLogDb> lectionLog = new() {
                new DataAccess.Models.LectionLogDb{StudentId = 1, LectionId = 1, Attendance = true, HomeworkId = 1, Score = 5}, 
                new DataAccess.Models.LectionLogDb{StudentId = 1, LectionId = 2, Attendance = false, HomeworkId = 1, Score = 0}
            };

            //await dbContext.Students.AddRangeAsync(students);
            dbContext.Students.AddRange(students);
            dbContext.Lections.AddRange(lections);
            dbContext.Lectors.AddRange(lectors);
            dbContext.Homeworks.AddRange(homework);
            dbContext.LectionLogs.AddRange(lectionLog);

            //await dbContext.SaveChangesAsync();
            dbContext.SaveChanges();

            HttpClient httpClient = webHost.CreateClient();
            _httpClient = httpClient;
        }
        

        [TestCase(true,true,"Ds","Ds")]
        [TestCase(true,false,"Ds","fgfdgdf")]
        [TestCase(false,true,"cghbfchg","Ds")]
        [TestCase(false,false,"","")]
        public async Task PostReport_SendRequest_ShouldReturnOkResponse(bool lectionAttendance, bool studentAttendance, string lectionName, string studentName)
        {
            HttpContent content = new StringContent("");
            string request = $"?lectionAttendance={lectionAttendance.ToString()}" +
                             $"&studentAttendance={studentAttendance.ToString()}" +
                             $"&lectionName={lectionName}" +
                             $"&studentName={studentName}";
            // Act
            HttpResponseMessage responseMessage = await _httpClient.PostAsync($"api/attendance_report/create-report{request}",content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
        }
        
        [TestCase(true,true,"","")]
        [TestCase(true,false,"sdfg","")]
        [TestCase(false,true,"","sdfg")]
        public async Task PostReport_SendRequest_ShouldReturnNotFoundResponse(bool lectionAttendance, bool studentAttendance, string lectionName, string studentName)
        {
            HttpContent content = new StringContent("");
            string request = $"?lectionAttendance={lectionAttendance.ToString()}" +
                             $"&studentAttendance={studentAttendance.ToString()}" +
                             $"&lectionName={lectionName}" +
                             $"&studentName={studentName}";
            // Act
            HttpResponseMessage responseMessage = await _httpClient.PostAsync($"api/attendance_report/create-report{request}",content);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, responseMessage.StatusCode);
        }
    }
}