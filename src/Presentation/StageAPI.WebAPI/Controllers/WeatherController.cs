using Microsoft.AspNetCore.Mvc;
using StageAPI.Application.Services;

namespace StageAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string city)
        {
            return Ok(await _weatherService.GetWeatherAsync(city));
        }
        [HttpGet("daily")]
        public async Task<IActionResult> GetWeatherForDaily([FromQuery] string city, string datetime)
        {
            return Ok(await _weatherService.GetWeatherAsync(city, datetime));
        }
    }
}