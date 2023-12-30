namespace StageAPI.Application.Services
{
    // Service interface for weather-related functionality
    public interface IWeatherService
    {
        /// <summary>
        /// Asynchronously retrieves weather information for the specified city
        /// </summary>
        /// <param name="city">The city for which weather information is to be obtained</param>
        /// <returns>A task representing the asynchronous operation that returns a JSON string containing weather information</returns>
        Task<string> GetWeatherAsync(string city);
    }
}