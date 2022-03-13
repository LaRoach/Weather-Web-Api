using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherWebApi.Models.Users;
using WeatherWebApi.Services.Interfaces;

namespace WeatherWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Consumes("multipart/form-data")]
        [Route("/user")]
        public async Task<IActionResult> RegisterUser([FromForm] UserRegisterRequest user)
        {
            await _userService.RegisterUser(user);
            return Ok(new { message = "Registration successful" });
        }

        [AllowAnonymous]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Route("/auth")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginRequest user)
        {
            var response = await _userService.LoginUser(user);
            return Ok(response);
        }


        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [Route("/profile")]
        public async Task<UserProfileResponse> LoadProfilePage()
        {
            return await _userService.LoadProfilePage();
        }
    }
}