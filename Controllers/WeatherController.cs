using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeatherFront.Models;
using WeatherFront.Services;

namespace WeatherFront.Controllers
{
    public class WeatherController : ApiController
    {
        private readonly IWeatherService weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        /// <summary>
        /// Get the current weather state for the specified city.
        /// </summary>
        /// <param name="city">The name of the city to look up.</param>
        /// <param name="country">The 2 digit country code in which the city is located.</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(WeatherState), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<WeatherState>> GetWeather([FromQuery] string city, [FromQuery] string country)
        {
            // validate required query parameters
            if (string.IsNullOrEmpty(city))
                return BadRequest("City name was not specified.");
            if (string.IsNullOrEmpty(country))
                return BadRequest("Country code was not specified.");

            var result = await weatherService.GetWeatherAsync(city, country);

            return Ok(result);
        }
    }
}
