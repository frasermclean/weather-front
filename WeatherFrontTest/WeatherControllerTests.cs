using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using WeatherFront.Controllers;
using WeatherFront.Models;
using WeatherFront.Services;
using Xunit;

namespace WeatherFrontTest
{
    public class WeatherControllerTests
    {
        // mocked services
        private readonly Mock<IWeatherService> weatherService;
        private readonly Mock<IKeyService> keyService;
        private readonly WeatherController controller;

        public WeatherControllerTests()
        {
            // setup key service mock
            keyService = new Mock<IKeyService>();
            keyService.Setup(s => s.IsKeyKnown("abc")).Returns(true);
            keyService.Setup(s => s.UseKey("abc")).Returns((true, string.Empty));
            keyService.Setup(s => s.IsKeyKnown("xyz")).Returns(false);
            keyService.Setup(s => s.UseKey("xyz")).Returns((false, string.Empty));

            // setup weather service mock
            weatherService = new Mock<IWeatherService>();
            weatherService.Setup(s => s.GetWeatherAsync("Melbourne", "au")).ReturnsAsync(new WeatherState()
            {
                City = "Melbourne",
                Country = "AU",
                Description = "sunny",
                Temperature = 23.1
            });

            // create weather controller
            controller = new(weatherService.Object, keyService.Object);
        }

        [Fact(DisplayName = "Get Melbourne weather with valid API key.")]
        public async Task GetMelbourneWeatherWithValidKeyAsync()
        {
            var actionResult = await controller.LookupWeatherStateAsync(new WeatherRequestBody()
            {
                City = "Melbourne",
                Country = "au",
                ApiKey = "abc"
            });

            Assert.True(actionResult.Result is OkObjectResult, "Controller returns OK (200)");
        }

        [Fact(DisplayName = "Get Melbourne weather with invalid API key.")]
        public async Task GetMelbourneWeatherWithInvalidKeyAsync()
        {
            var actionResult = await controller.LookupWeatherStateAsync(new WeatherRequestBody()
            {
                City = "Melbourne",
                Country = "au",
                ApiKey = "xyz"
            });

            Assert.True(actionResult.Result is UnauthorizedObjectResult, "Controller returns Unauthorized (401)");
        }
    }
}
