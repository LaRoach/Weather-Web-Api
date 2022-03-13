using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using WeatherWebApi.Utils;

namespace WeatherWebApi.Models.Users
{
    public class UserRegisterRequest
    {   
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg" })]
        public IFormFile Image { get; set; }
    }
}