using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using WeatherWebApi.Models.Users;
using WeatherWebApi.Tests.Shared;
using Xunit;

namespace WeatherWebApi.Tests.WeatherControllerTests
{
    public class WeatherControllerTest : IntegrationTest
    {
        [Fact]
        public async Task Call_WeatherData_UnAuthorized()
        {
            // Arrange

            // Act
            var response = await TestClient.GetAsync("/weather/london");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Call_WeatherData_Authorized()
        {
            // Arrange  
            var userRegister = new UserRegisterRequest
            {
                FirstName = "Darth",
                LastName = "Vader",
                Email = "darth@email.com",
                Password = "password",
            };

            // Act
            await RegisterUserAsync(userRegister);

            await AuthenticateAsync();

            var response = await TestClient.GetAsync("/weather/london");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Call_WeatherData_Authorized_BadCityData()
        {
            // Arrange  
            var userRegister = new UserRegisterRequest
            {
                FirstName = "Darth",
                LastName = "Vader",
                Email = "darth@email.com",
                Password = "password",
            };

            // Act
            await RegisterUserAsync(userRegister);

            await AuthenticateAsync();

            var response = await TestClient.GetAsync("/weather/fakecity");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}