using System;
using System.Dynamic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WeatherWebApi.Services.Interfaces;
using WeatherWebApi.Utils;

namespace WeatherWebApi.Services.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public WeatherService(IConfiguration config, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
        }

        public async Task<ExpandoObject> LoadLiveWeatherData(string cityName)
        {
            if (string.IsNullOrEmpty(cityName))
            {
                throw new ArgumentNullException(nameof(cityName));
            }

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"http://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={Environment.GetEnvironmentVariable("WeatherApiKey") ?? _config.GetSection("WeatherApiKey").Value}");
                var data = JsonSerializer.Deserialize<ExpandoObject>(await response.Content.ReadAsStringAsync());
                if (response.IsSuccessStatusCode)
                {
                    return (data);
                }

                throw new CustomException(await response.Content.ReadAsStringAsync() ?? response.ReasonPhrase, (int)response.StatusCode);
            }
        }
    }
}