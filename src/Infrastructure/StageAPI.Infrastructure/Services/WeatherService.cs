using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using StageAPI.Application.DTOs.Weather;
using StageAPI.Application.Services;

namespace StageAPI.Infrastructure.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<WeatherService> _logger;
        private readonly RestClient _client;

        public WeatherService(ILogger<WeatherService> logger, IConfiguration configuration, RestClient client)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        /// <summary>
        /// Retrieves weather information asynchronously for the specified city
        /// </summary>
        /// <param name="city">The city for which weather information is to be obtained</param>
        /// <returns>A JSON string containing weather information</returns>
        /// <exception cref="Exception">Thrown when there is a failure to retrieve weather information</exception>
        public async Task<string> GetWeatherAsync(string city)
        {
            try
            {
                // Retrieves the API URL from configuration settings
                string apiUrl = _configuration["CollectAPI:ApiUrl"] ?? throw new InvalidOperationException("API Url is missing in configuration"); ;
                // Retrieves the API key from configuration settings
                string apikey = _configuration["CollectAPI:ApiKey"] ?? throw new InvalidOperationException("API Key is missing in configuration");
                // Creates a RestRequest for the REST call
                var request = new RestRequest(apiUrl, Method.Get)
                    .AddParameter("data.lang", "tr")
                    .AddParameter("data.city", city)
                    .AddHeader("authorization", apikey)
                    .AddParameter("content-type", "application/json");
                //
                RestResponse restResponse = await _client.ExecuteAsync(request);

                // If the REST call is unsuccessful, logs the error and throws an exception
                if (!restResponse.IsSuccessful)
                {
                    _logger.LogError($"Failed to retrieve weather data. StatusCode: {restResponse.StatusCode}, ErrorMessage: {restResponse.ErrorMessage}");
                    throw new Exception("Failed to retrieve weather data");
                }
                // In case of success, logs the response content and returns it
                _logger.LogInformation(restResponse.Content);
                return restResponse.Content ?? string.Empty;
            }
            catch (Exception ex)
            {
                // Logs errors in case of exceptions and throws an exception
                _logger.LogError(ex, $"An error occured in GetWeatherAsync: {ex.Message}");
                throw new Exception("An error occured in GetWeatherAsync");
            }
        }

        public async Task<WeatherInfo> GetWeatherAsync(string city, string datetime)
        {
            try
            {
                // Call the GetWeatherAsync method to retrieve the weather data
                string weatherData = await GetWeatherAsync(city);

                // Deserialize the JSON response into a strongly typed object
                WeatherApiResponse weatherApiResponse = JsonConvert.DeserializeObject<WeatherApiResponse>(weatherData);

                // Find the weather information for the target date
                WeatherInfo targetWeather = weatherApiResponse?.Result.FirstOrDefault(weather => weather.Date == datetime);
                targetWeather.City = weatherApiResponse.City;
                if (targetWeather != null)
                {
                    // Log the relevant information and return the weather details
                    _logger.LogInformation($"Weather for {city} on {datetime}: {targetWeather.Description}, Temperature: {targetWeather.Degree}°C");
                    return targetWeather;
                }
                else
                {
                    // Log a message if the weather information for the target date is not found
                    _logger.LogWarning($"Weather information for {city} on {datetime} not found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and log errors
                _logger.LogError(ex, $"An error occurred in GetWeatherForDateAsync: {ex.Message}");
                throw new Exception("An error occurred in GetWeatherForDateAsync");
            }
        }
    }
}