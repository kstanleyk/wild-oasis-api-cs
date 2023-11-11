using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace WildOasis.API.Services;

public class MessageProcessingServiceBase : BackgroundService
{
    private readonly int _refreshIntervalInSeconds;

    public MessageProcessingServiceBase()
    {
        _refreshIntervalInSeconds = 30;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            //var forecast = await _weatherApiClient.GetWeatherForecastAsync(stoppingToken);

            //if (forecast is object)
            //{
            //    var currentWeather = new CurrentWeatherResult { Description = forecast.Weather.Description };

            //    var cacheKey = $"current_weather_{DateTime.UtcNow:yyyy_MM_dd}";

            //    _logger.LogInformation("Updating weather in cache.");

            //    await _cache.SetAsync(cacheKey, currentWeather, _minutesToCache);
            //}

            await Task.Delay(TimeSpan.FromSeconds(_refreshIntervalInSeconds), stoppingToken);
        }
    }
}