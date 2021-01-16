using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chart.API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CovidsController : ControllerBase
    {
        private readonly CovidService _service;
        public CovidsController(CovidService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> SaveCovid(Covid covid)
        {
            await _service.SaveCovid(covid);
            return Ok(_service.GetCovidChartList());
        }

        [HttpGet]
        public IActionResult InitializeCovid()
        {
            Random rnd = new Random();

            Enumerable.Range(1, 10).ToList().ForEach(x =>
            {
                foreach (CityEnum item in Enum.GetValues(typeof(CityEnum)))
                {
                    var newCovid = new Covid
                    {
                        City = item,
                        Count = rnd.Next(250, 2000),
                        CovidDate = DateTime.Now.AddDays(x),
                    };
                    _service.SaveCovid(newCovid).Wait();
                    System.Threading.Thread.Sleep(1000);
                }
            });

            return Ok("Veriler başarıyla kaydedildi");
        }
    }
}