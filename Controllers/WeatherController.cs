using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherFront.Models;

namespace WeatherFront.Controllers
{
    public class WeatherController : ApiController
    {
        /// <summary>
        /// Get the current weather state for the specified city.
        /// </summary>
        /// <param name="city">The name of the city to look up.</param>
        /// <param name="country">The 2 digit country code in which the city is located.</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(WeatherState), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public ActionResult<WeatherState> GetWeather([FromQuery]string city, [FromQuery]string country)
        {
            // validate required query parameters
            if (string.IsNullOrEmpty(city))
                return BadRequest("City name was not specified.");
            if (string.IsNullOrEmpty(country))
                return BadRequest("Country code was not specified.");

            return Ok(new WeatherState()
            {
                City = city,
                Country = country,
                Description = "Pretty good",
                Temperature = 23.0f,
            });
        }
    }
}
