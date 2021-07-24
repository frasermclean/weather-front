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
        private readonly IKeyService keyService;

        public WeatherController(IWeatherService weatherService, IKeyService keyService)
        {
            this.weatherService = weatherService;
            this.keyService = keyService;
        }

        /// <summary>
        /// Get the current weather state for the specified city.
        /// </summary>
        /// <param name="city">The name of the city to look up.</param>
        /// <param name="country">The 2 digit country code in which the city is located.</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(WeatherState), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<WeatherState>> LookupWeatherStateAsync([FromBody] WeatherRequestBody body)
        {
            // validate that api key exists
            if (!keyService.IsKeyKnown(body.ApiKey))
                return Unauthorized($"Unknown API key: {body.ApiKey}");

            // attempt to use the API key
            var (success, message) = keyService.UseKey(body.ApiKey);
            if (!success)
                return BadRequest(message);

            // use service to look up current weather state
            WeatherState state = await weatherService.GetWeatherAsync(body.City, body.Country);
            return state != null ? 
                Ok(state) : 
                NotFound($"Could not find a city with name: {body.City} in country code: {body.Country}.");
        }
    }
}
