namespace WeatherFront.Services
{
    public interface IKeyService
    {
        /// <summary>
        /// Returns true if specified API key is known.
        /// </summary>
        /// <param name="key">The API key to look up.</param>
        /// <returns>True if known, false if not.</returns>
        bool IsKeyDefined(string key);
    }
}
