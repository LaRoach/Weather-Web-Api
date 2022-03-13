using System.Threading.Tasks;
using WeatherWebApi.Models.Users;

namespace WeatherWebApi.Data.Interfaces
{
    public interface IUserRepository
    {
        Task RegisterUser(User user);

        Task<bool> CheckIfUserExists(string email);

        Task<User> GetUser(string email);
    }
}