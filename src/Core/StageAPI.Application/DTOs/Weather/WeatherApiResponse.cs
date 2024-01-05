namespace StageAPI.Application.DTOs.Weather
{
    public class WeatherApiResponse
    {
        public bool Success { get; set; }
        public string City { get; set; }
        public List<WeatherInfo> Result { get; set; }
    }
}