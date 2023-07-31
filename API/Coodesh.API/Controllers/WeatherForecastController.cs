using Coodesh.Infrastructure;
using Coodesh.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Coodesh.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]{"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"};
        private readonly ProductContext _productContext;
        public WeatherForecastController(ProductContext ctx)
        {
            _productContext = ctx;
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

        [HttpPost]
        public IActionResult Create(Product p)
        {
            _productContext.Add(p);
            _productContext.SaveChanges();
            return Ok();
        }
    }
}