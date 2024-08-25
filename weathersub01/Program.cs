using Microsoft.AspNetCore.SignalR.Client;
using System.Text.Json;

Console.WriteLine("Weather sub 01");

var connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7065/forecast")
                .WithAutomaticReconnect()
                .Build();

connection.On<string>("ReceiveWeatherData", data =>
{
    var weather = JsonSerializer.Deserialize<dynamic>(data);
    Console.WriteLine(weather);
    Console.WriteLine();
});

connection.Reconnecting += async error =>
{
    Console.WriteLine("Connection lost. Reconnecting ...");
    await Task.CompletedTask;
};

try
{
    await connection.StartAsync();
    Console.WriteLine("Connection started on Weather sub 01.");

    await connection.InvokeAsync("WeatherDataAsync");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}

Console.ReadLine();
