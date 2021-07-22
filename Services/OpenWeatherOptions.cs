using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherFront.Services
{
    public class OpenWeatherOptions
    {
        public const string Section = "OpenWeather";

        public string[] ApiKeys { get; set; }
    }
}
