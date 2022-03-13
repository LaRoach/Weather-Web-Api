using System.Dynamic;
using System.Threading.Tasks;

namespace WeatherWebApi.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<ExpandoObject> LoadLiveWeatherData(string cityName);
    }
}