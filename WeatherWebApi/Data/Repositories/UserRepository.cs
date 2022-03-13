using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherWebApi.Data.Interfaces;
using WeatherWebApi.Models.Users;

namespace WeatherWebApi.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task RegisterUser(User user)
        {
            await _applicationDbContext.Users.AddAsync(user);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckIfUserExists(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            return await _applicationDbContext.Users.Where(u => u.Email.Equals(email)).AnyAsync();
        }

        public async Task<User> GetUser(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            return await _applicationDbContext.Users.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync();
        }
    }
}