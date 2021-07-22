using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using WeatherFront.Configuration;

namespace WeatherFront.Services
{
    public class KeyService : IKeyService
    {
        public int RequestsPerHour { get; init; }
        public IReadOnlyList<string> ValidKeys { get; init; }
            
        public KeyService(IConfiguration configuration)
        {
            // read values from configuration
            KeyServiceOptions options = new();
            configuration.GetSection(KeyServiceOptions.Section).Bind(options);

            // set service properties
            RequestsPerHour = options.RequestsPerHour;
            ValidKeys = new List<string>(options.ValidKeys);
        }
        
        public bool IsKeyDefined(string key)
        {
            return ValidKeys.Contains(key);
        }
    }
}
