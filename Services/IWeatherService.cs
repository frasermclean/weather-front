using System.Threading.Tasks;
using WeatherFront.Models;

namespace WeatherFront.Services
{
    public interface IWeatherService
    {
        Task<WeatherState> GetWeatherAsync(string city, string country);
    }
}
