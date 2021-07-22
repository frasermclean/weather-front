using System.Collections.Generic;

namespace WeatherFront.Configuration
{
    public class KeyServiceOptions
    {
        public const string Section = "KeyService";

        public int RequestLimit { get; init; }
        public int ExpirySeconds { get; init; }
        public HashSet<string> ValidKeys { get; init; }
    }
}
