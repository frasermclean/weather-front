using System.Threading.Tasks;
using WeatherFront.Models;

namespace WeatherFront.Services
{
    public class OpenWeatherService : IWeatherService
    {
        public Task<WeatherState> GetWeatherAsync(string city, string country)
        {
            return Task.FromResult(new WeatherState()
            {
                City = city,
                Country = country,
                Description = "Pretty good",
                Temperature = 23.1f,
            });
        }
    }
}
