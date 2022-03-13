
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WeatherWebApi.Data;
using WeatherWebApi.Models.Users;

namespace WeatherWebApi.Tests.Shared
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                    {
                        builder.ConfigureServices(services =>
                        {
                            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                            if (descriptor != null)
                            {
                                services.Remove(descriptor);
                            }

                            services.AddDbContext<ApplicationDbContext>(options => { options.UseInMemoryDatabase("TestDb"); });
                        });
                    });

            TestClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await LoginAndGetJwtAsync());
        }

        protected async Task RegisterUserAsync(UserRegisterRequest userRegisterRequest)
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy image file");

            var multipartFormContent = new MultipartFormDataContent();

            multipartFormContent.Add(new StringContent(userRegisterRequest.FirstName), name: "firstName");
            multipartFormContent.Add(new StringContent(userRegisterRequest.LastName), name: "lastName");
            multipartFormContent.Add(new StringContent(userRegisterRequest.Email), name: "email");
            multipartFormContent.Add(new StringContent(userRegisterRequest.Password), name: "password");

            var fileStreamContent = new StreamContent(new MemoryStream(bytes));
            fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            multipartFormContent.Add(fileStreamContent, name: "image", fileName: "testimage.png");

            await TestClient.PostAsync("/user", multipartFormContent);
        }

        private async Task<string> LoginAndGetJwtAsync()
        {
            var response = await TestClient.PostAsJsonAsync("/auth", new UserLoginRequest
            {
                Email = "darth@email.com",
                Password = "password"
            });

            var loginResponse = await response.Content.ReadFromJsonAsync<UserLoginResponse>();
            return loginResponse.Token;
        }
    }
}