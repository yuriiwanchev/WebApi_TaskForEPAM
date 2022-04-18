using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace RestApi.IntegrationTests
{
    [TestFixture]
    public class CrudControllersTests
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

            //await dbContext.Students.AddRangeAsync(students);
            dbContext.Students.AddRange(students);
            dbContext.Lections.AddRange(lections);
            dbContext.Lectors.AddRange(lectors);
            dbContext.Homeworks.AddRange(homework);

            //await dbContext.SaveChangesAsync();
            dbContext.SaveChanges();

            HttpClient httpClient = webHost.CreateClient();
            _httpClient = httpClient;
        }
        
        
        private static readonly List<TestCaseData> DataForControllers =
            new List<TestCaseData>(new[]
            {
                new TestCaseData("student", new StringContent(
                    JsonSerializer.Serialize(
                        new Student(3,"Dsds","sdf@ya.ru","+7 (924) 224-53-12")),
                        Encoding.UTF8,
                        "application/json")),
                new TestCaseData("lector", new StringContent(
                    JsonSerializer.Serialize(
                        new Lector(3,"Dsds","sdf@ya.ru")),
                        Encoding.UTF8,
                        "application/json")),
                new TestCaseData("lection", new StringContent(
                    JsonSerializer.Serialize(
                        new Lection(3,"Dsds",2,DateTime.Now)),
                        Encoding.UTF8,
                        "application/json")),
                new TestCaseData("homework", new StringContent(
                    JsonSerializer.Serialize(
                        new Homework(3,"Dsds")),
                        Encoding.UTF8,
                        "application/json")),
            });
        
        // [TestCase("student")]
        // [TestCase("lector")]
        // [TestCase("lection")]
        // [TestCase("homework")]
        [TestCaseSource(nameof(DataForControllers)), Order(1)]
        public async Task GetStudentById_SendRequest_ShouldReturnOkResponse(string controllerPath, HttpContent content)
        {
            // Act
            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"api/{controllerPath}/1");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
        }
        
        [TestCaseSource(nameof(DataForControllers)), Order(2)]
        public async Task GetStudentById_SendWrongRequest_ShouldReturnNotFoundResponse(string controllerPath, HttpContent content)
        {
            // Act
            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"api/{controllerPath}/404");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, responseMessage.StatusCode);
        }
        
        [TestCaseSource(nameof(DataForControllers)), Order(3)]
        public async Task GetStudentsById_SendRequest_ShouldReturnOkResponse(string controllerPath, HttpContent content)
        {
            // Act
            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"api/{controllerPath}/");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
        }
        
        [TestCaseSource(nameof(DataForControllers)), Order(4)]
        public async Task PostStudent_SendRequest_ShouldReturnOkResponse(string controllerPath, HttpContent content)
        {
            // HttpContent content = new StringContent(
            //     JsonSerializer.Serialize(new Student(3,"Dsds","sdf@ya.ru","+7 (924) 224-53-12")),
            //     Encoding.UTF8,
            //     "application/json");
            // Act
            HttpResponseMessage responseMessage = await _httpClient.PostAsync($"api/{controllerPath}/", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
        }
        
        [TestCaseSource(nameof(DataForControllers)), Order(5)]
        public async Task PostStudent_SendExistingStudent_ShouldReturnNotAcceptableResponse(string controllerPath, HttpContent content)
        {
            
            // HttpContent content = new StringContent(
            //     JsonSerializer.Serialize(new Student(1,"Dsds","sdf@ya.ru","+7 (924) 224-53-12")),
            //     Encoding.UTF8,
            //     "application/json");
            // Act
            HttpResponseMessage responseMessage = await _httpClient.PostAsync($"api/{controllerPath}/", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotAcceptable, responseMessage.StatusCode);
        }
        
        [TestCaseSource(nameof(DataForControllers)), Order(6)]
        public async Task PutStudentUpdateById_SendRequest_ShouldReturnOkResponse(string controllerPath, HttpContent content)
        {
            
            // HttpContent content = new StringContent(
            //     JsonSerializer.Serialize(new Student(0,"Dsds","sdf@ya.ru","+7 (924) 224-53-12")),
            //     Encoding.UTF8,
            //     "application/json");
            // Act
            HttpResponseMessage responseMessage = await _httpClient.PutAsync($"api/{controllerPath}/3",content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
        }
        
        [TestCaseSource(nameof(DataForControllers)), Order(8)]
        public async Task PutStudentUpdateById_SendRequest_ShouldReturnNotFoundResponse(string controllerPath, HttpContent content)
        {
            // HttpContent content = new StringContent(
            //     JsonSerializer.Serialize(new Student(0,"Dsds","sdf@ya.ru","+7 (924) 224-53-12")),
            //     Encoding.UTF8,
            //     "application/json");
            // Act
            HttpResponseMessage responseMessage = await _httpClient.PutAsync($"api/{controllerPath}/3",content);
            
            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, responseMessage.StatusCode);
        }
        
        [TestCaseSource(nameof(DataForControllers)), Order(7)]
        public async Task DeleteStudent_SendRequest_ShouldReturnOkResponse(string controllerPath, HttpContent content)
        {
            // Act
            HttpResponseMessage responseMessage = await _httpClient.DeleteAsync($"api/{controllerPath}/3");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
        }
        
        [TestCaseSource(nameof(DataForControllers)), Order(9)]
        public async Task DeleteStudent_SendRequest_ShouldReturnNotFoundResponse(string controllerPath, HttpContent content)
        {
            // Act
            HttpResponseMessage responseMessage = await _httpClient.DeleteAsync($"api/{controllerPath}/404");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, responseMessage.StatusCode);
        }
    }
}

