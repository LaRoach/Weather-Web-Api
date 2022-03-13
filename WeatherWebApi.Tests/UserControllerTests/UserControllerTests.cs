using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using WeatherWebApi.Models.Users;
using WeatherWebApi.Tests.Shared;
using Xunit;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace WeatherWebApi.Tests.UserControllerTests
{
    public class UserControllerTests : IntegrationTest
    {
        [Fact]
        public async Task Call_UserProfile_UnAuthorized()
        {
            // Arrange

            // Act
            var response = await TestClient.GetAsync("/profile");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Call_UserProfile_Authorized()
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

            var response = await TestClient.GetAsync("/profile");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var profileData = await response.Content.ReadFromJsonAsync<UserProfileResponse>();
            Assert.True(profileData.Email.Equals("darth@email.com"));
            Assert.True(profileData.FirstName.Equals("Darth"));
            Assert.True(profileData.LastName.Equals("Vader"));
            Assert.Contains("darth@email.com", profileData.ImageLocation);
        }

        [Fact]
        public async Task Call_UserProfile_InvalidJWT()
        {
            // Arrange  
            //set the jwt token as the default one from jwt.io
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");

            // Act
            var response = await TestClient.GetAsync("/profile");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}