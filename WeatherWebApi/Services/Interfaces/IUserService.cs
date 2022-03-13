using System.Threading.Tasks;
using WeatherWebApi.Models.Users;

namespace WeatherWebApi.Services.Interfaces
{
    public interface IUserService
    {
        Task RegisterUser(UserRegisterRequest user);
        Task<UserLoginResponse> LoginUser(UserLoginRequest user);
        Task<UserProfileResponse> LoadProfilePage();
    }
}