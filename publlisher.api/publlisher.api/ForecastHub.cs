using Microsoft.AspNetCore.SignalR;
using publlisher.api;
using System.Text.Json;

namespace publisher.api
{
    public class ForecastHub : Hub
    {
        private readonly IHubContext<ForecastHub> _hubContext;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        public ForecastHub(IHubContext<ForecastHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task WeatherDataAsync()
        {
            var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
            await _hubContext.Clients.All.SendAsync("ReceiveWeatherData", JsonSerializer.Serialize(data));
        }
    }
}
