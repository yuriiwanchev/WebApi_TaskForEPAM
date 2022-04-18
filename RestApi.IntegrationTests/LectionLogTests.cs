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
    public class LectionLogTests
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
        
        
        private static readonly List<TestCaseData> DataForControllers =
            new List<TestCaseData>(new[]
            {
                new TestCaseData("lector_log", new StringContent(
                    JsonSerializer.Serialize(
                        new LectionLog(2, 2, true,  2, 5)),
                        Encoding.UTF8,
                        "application/json")),
            });
        
        [TestCaseSource(nameof(DataForControllers)), Order(1)]
        public async Task GetLogById_SendRequest_ShouldReturnOkResponse(string controllerPath, HttpContent content)
        {
            // Act
            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"api/{controllerPath}/1,1,1");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
        }
        
        [TestCaseSource(nameof(DataForControllers)), Order(2)]
        public async Task GetLogById_SendWrongRequest_ShouldReturnNotFoundResponse(string controllerPath, HttpContent content)
        {
            // Act
            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"api/{controllerPath}/4,0,4");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, responseMessage.StatusCode);
        }
        
        [TestCaseSource(nameof(DataForControllers)), Order(3)]
        public async Task GetLogsById_SendRequest_ShouldReturnOkResponse(string controllerPath, HttpContent content)
        {
            // Act
            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"api/{controllerPath}/");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
        }
        
        [TestCaseSource(nameof(DataForControllers)), Order(4)]
        public async Task PostLog_SendRequest_ShouldReturnOkResponse(string controllerPath, HttpContent content)
        {
            // Act
            HttpResponseMessage responseMessage = await _httpClient.PostAsync($"api/{controllerPath}/", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
        }
        
        [TestCaseSource(nameof(DataForControllers)), Order(5)]
        public async Task PostLog_SendExistingStudent_ShouldReturnNotAcceptableResponse(string controllerPath, HttpContent content)
        {
            // Act
            HttpResponseMessage responseMessage = await _httpClient.PostAsync($"api/{controllerPath}/", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotAcceptable, responseMessage.StatusCode);
        }
        
        [TestCaseSource(nameof(DataForControllers)), Order(6)]
        public async Task PutLogUpdateById_SendRequest_ShouldReturnOkResponse(string controllerPath, HttpContent content)
        {
            // Act
            HttpResponseMessage responseMessage = await _httpClient.PutAsync($"api/{controllerPath}/2,2,2",content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
        }
        
        [TestCaseSource(nameof(DataForControllers)), Order(8)]
        public async Task PutLogUpdateById_SendRequest_ShouldReturnNotFoundResponse(string controllerPath, HttpContent content)
        {
            
            // Act
            HttpResponseMessage responseMessage = await _httpClient.PutAsync($"api/{controllerPath}/2,2,2",content);
            
            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, responseMessage.StatusCode);
        }
        
        [TestCaseSource(nameof(DataForControllers)), Order(7)]
        public async Task DeleteLog_SendRequest_ShouldReturnOkResponse(string controllerPath, HttpContent content)
        {
            // Act
            HttpResponseMessage responseMessage = await _httpClient.DeleteAsync($"api/{controllerPath}/2,2,2");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
        }
        
        [TestCaseSource(nameof(DataForControllers)), Order(9)]
        public async Task DeleteLog_SendRequest_ShouldReturnNotFoundResponse(string controllerPath, HttpContent content)
        {
            // Act
            HttpResponseMessage responseMessage = await _httpClient.DeleteAsync($"api/{controllerPath}/4,0,4");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, responseMessage.StatusCode);
        }
    }
}