using Microsoft.AspNetCore.Mvc;
using TodoLists.Queries.WeatherForecasts.GetWeatherForecasts;

namespace CleanArchitecture.WebUI.Controllers;

public class WeatherForecastController : ApiControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        return await Mediator.Send(new GetWeatherForecastsQuery());
    }
}
