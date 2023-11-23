using Microsoft.AspNetCore.Mvc;
using PocRedis.Services;
using System.Diagnostics;

namespace PocRedis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var customers = await _customerService.GetCustomers();

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            var result = new
            {
                duration = string.Format("{0:00}:{1:00}.{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds),
                customers
            };

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var customer = await _customerService.GetCustomer(id);

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            var result = new
            {
                duration = string.Format("{0:00}:{1:00}.{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds),
                customer
            };

            return Ok(result);
        }

        [HttpGet]
        [Route("external")]
        public async Task<IActionResult> GetExternalCustomer()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var customers = await _customerService.GetExternalCustomers();

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            var result = new
            {
                duration = string.Format("{0:00}:{1:00}.{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds),
                customers
            };

            return Ok(result);
        }

    }
}
