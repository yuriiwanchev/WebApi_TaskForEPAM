using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Moq;
using RestApi.Controllers;
using Xunit;


namespace RestApi.Tests
{
    public class StudentControllerTests// : IClassFixture<ApiWebApplicationFactory>
    {
        // [Fact]
        // public void Get_order_detail_success()
        // {
        //     //Arrange
        //     var fakeOrderId = "1";
        //     // var fakeOrder = GetFakeOrder();
        //     
        //     //...
        //     
        //     var studentServiceMock = new Mock<ICrudService<Student>>();
        //     var logger = new Mock<ILogger<StudentController>>();
        //
        //     var contextMock = new Mock<HttpContext>();
        //     
        //     //Act
        //     var studentController = new StudentController(studentServiceMock.Object, logger.Object);
        //
        //     studentController.ControllerContext.HttpContext = contextMock.Object;
        //
        //     var actionResult = studentController.GetStudent(1);
        //     
        //     //Assert
        //     var viewResult = Assert.IsType<ViewResult>(actionResult);
        //     Assert.IsAssignableFrom<Student>(viewResult.ViewData.Model);
        // }
        //
        [Fact]
        public async Task GET_retrieves_weather_forecast()
        {
            using var application = new WebApplicationFactory<Startup>();
            using var client = application.CreateClient();
        
            var response = await client.GetAsync("/api/student");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        // private readonly HttpClient _client;
        //
        // public UnitTest1()
        // {
        //     var appfactory = new WebApplicationFactory<Startup>();
        //     _client = appfactory.CreateClient();
        // }
        //
        // [Fact]
        // public void Tst()
        // {
        //     _client.GetAsync(ApiRou)
        // }
        
        
    }

}

