using Microsoft.AspNetCore.Mvc;
using publisher.api;
using System.Text;
using System.Text.Json;

namespace publlisher.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ForecastHub _hub;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ForecastHub hub)
        {
            _logger = logger;
            _hub = hub;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        
        [HttpGet("PublishWeatherForecast")]
        public async Task<bool> Pub()
        {
            await _hub.WeatherDataAsync();
            return true;
        }
    }
}
