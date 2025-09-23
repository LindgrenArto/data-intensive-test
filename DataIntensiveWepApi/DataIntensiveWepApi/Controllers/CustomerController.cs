using DataIntensiveWepApi.Models;
using DataIntensiveWepApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataIntensiveWepApi.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : Controller
    {

        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{db}")]
        public IActionResult GetCustomers([FromRoute] int db) {

            try
            {
                List<Customer> customers = _customerService.GetCustomers(db);

                return Ok(customers);
            }
            catch (Exception e)
            {

                return StatusCode(500, e);
            }
        }
    }
}
