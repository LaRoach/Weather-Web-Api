using WeatherWebApi.Models.Users;

namespace WeatherWebApi.Utils.Authorization
{
    public interface IJwtHandler
    {
        string GenerateToken(User user);
    }
}