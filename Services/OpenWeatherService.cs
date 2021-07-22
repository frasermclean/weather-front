using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherFront.Configuration;
using WeatherFront.Models;

namespace WeatherFront.Services
{
    public class OpenWeatherService : IWeatherService
    {
        private readonly ILogger<OpenWeatherService> logger;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly string apiKey;

        public OpenWeatherService(IConfiguration configuration, ILogger<OpenWeatherService> logger, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;

            // read all api keys from app settings
            var options = new OpenWeatherOptions();
            configuration.GetSection(OpenWeatherOptions.Section).Bind(options);
            
            // select api key randomly from those available
            int index = new Random().Next(0, options.ApiKeys.Length);
            apiKey = options.ApiKeys[index];
        }

        public async Task<WeatherState> GetWeatherAsync(string city, string country)
        {
            logger.LogInformation($"Attempting to look up city: {city}, from country code: {country}.");

            // create http client to send request to OpenWeather API
            using HttpClient client = httpClientFactory.CreateClient();
            string requestUri = $"https://api.openweathermap.org/data/2.5/weather?q={city},{country}&appid={apiKey}&units=metric";
            HttpResponseMessage response = await client.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                var contentStream = response.Content.ReadAsStream();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                // deserialize response
                OpenWeatherResponse obj = await JsonSerializer.DeserializeAsync<OpenWeatherResponse>(contentStream, options);

                // map response to correct return format
                return new WeatherState()
                {
                    City = obj.Name,
                    Country = obj.Sys.Country,
                    Description = obj.Weather[0].Description,
                    Temperature = obj.Main.Temp,
                };
            }

            return null;
        }
    }
}
