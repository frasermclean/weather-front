namespace WeatherFront.Services
{
    public interface IKeyService
    {
        /// <summary>
        /// Attempt to use the specified API key.
        /// </summary>
        /// <param name="key">The API key to look up.</param>
        /// <returns>A bool, string tuple with the bool indicating success. If there is an error,
        /// the string will contain details.</returns>
        (bool, string) UseKey(string key);
    }
}
