using Currencies.DomainServices;
using Currencies.Dtos;
using Currencies.Entities;
using Currencies.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using EasyNetQ;
using Newtonsoft;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Currencies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly ICurrenciesService currenciesService;

        public CurrenciesController(ICurrenciesService service)
        {
            currenciesService = service;
        }

        // GET: api/<CurrenciesController>
        [HttpGet]
        public List<CurrencyDto> Get()
        {
            return currenciesService.GetCurrencies(); ;
        }

        // GET api/<CurrenciesController>/5
        [HttpGet("{code}")]
        public List<CurrencyDto> Get(string code)
        {
            return currenciesService.GetCurrencies(code);
        }

        // POST api/<CurrenciesController>
        [HttpPost]
        public void Post()
        {
            currenciesService.ForceUpdateRates();
        }

        // PUT api/<CurrenciesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CurrenciesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
