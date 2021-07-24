using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using WeatherFront.Configuration;

namespace WeatherFront.Services
{
    public class KeyService : IKeyService
    {
        private readonly Dictionary<string, Queue<DateTime>> keyRequests = new();
        private readonly ILogger<KeyService> logger;

        /// <summary>
        /// Number of requests allowed within the expiry span.
        /// </summary>
        public int RequestLimit { get; init; }

        /// <summary>
        /// How long until a new request is allowed.
        /// </summary>
        public TimeSpan ExpirySpan { get; init; }

        /// <summary>
        /// Collection of authorized API keys.
        /// </summary>
        public IReadOnlyList<string> ValidKeys { get; init; }

        public KeyService(IConfiguration configuration, ILogger<KeyService> logger)
        {
            // read values from configuration
            KeyServiceOptions options = new();
            configuration.GetSection(KeyServiceOptions.Section).Bind(options);

            // set service properties
            RequestLimit = options.RequestLimit;
            ExpirySpan = TimeSpan.FromSeconds(options.ExpirySeconds);
            ValidKeys = new List<string>(options.ValidKeys);
            this.logger = logger;
        }

        public bool IsKeyKnown(string key)
        {
            return ValidKeys.Contains(key);
        }

        public (bool, string) UseKey(string key)
        {
            logger.LogInformation($"Validating authorization of API key: {key}");
            string message;

            // check that key is valid
            if (!IsKeyKnown(key))
            {
                message = $"Specified API key: {key} was not found";
                logger.LogWarning(message);
                return (false, message);
            }

            // create new queue when its the first request for this API key
            if (!keyRequests.ContainsKey(key))
                keyRequests[key] = new Queue<DateTime>();

            // clean up old queue items
            while (keyRequests[key].Count > 0)
            {
                var request = keyRequests[key].Peek();
                if (request + ExpirySpan < DateTime.Now)
                    keyRequests[key].Dequeue();
                else 
                    break;
            }

            // check if API key is under its limit
            if (keyRequests[key].Count < RequestLimit)
            {
                // enqueue new request
                keyRequests[key].Enqueue(DateTime.Now);
                message = $"API key: {key} has made {keyRequests[key].Count}/{RequestLimit} requests within allowed time span: {ExpirySpan.TotalSeconds} seconds.";
                logger.LogInformation(message);
                return (true, message);
            }
            else
            {
                // too many API requests
                message = $"Too many requests from API key: {key}";
                logger.LogWarning(message);
                return (false, message);
            }
        }
    }
}
