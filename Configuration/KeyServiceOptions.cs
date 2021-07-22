using System.Collections.Generic;

namespace WeatherFront.Configuration
{
    public class KeyServiceOptions
    {
        public const string Section = "KeyService";

        public int RequestsPerHour { get; init; }
        public HashSet<string> ValidKeys { get; init; }
    }
}
