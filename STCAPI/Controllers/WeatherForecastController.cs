using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using STAAPI.Infrastructure.Repository.GenericRepository;
using STCAPI.Core.Entities.Master;
using STCAPI.Core.ViewModel.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STCAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMapper _IMapper;
        private readonly IGenericRepository<DemoTable, int> _IDemoTableRepository;
        private readonly ILogger<WeatherForecastController> _logger;
        public WeatherForecastController(IMapper mapper, IGenericRepository<DemoTable, int> demoRepository,
            ILogger<WeatherForecastController> logger)
        {
            _IMapper = mapper;
            _IDemoTableRepository = demoRepository;
            _logger = logger;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            try
            {
                throw new Exception();
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message, ex);
            
            }
            var rng = new Random();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
