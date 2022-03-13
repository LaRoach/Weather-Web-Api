using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherWebApi.Services.Interfaces;

namespace WeatherWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [Route("/weather/{cityName}")]
        public async Task<IActionResult> LoadLiveWeatherData([FromRoute] string cityName)
        {
            var response = await _weatherService.LoadLiveWeatherData(cityName);
            return Ok(response);
        }
    }
}