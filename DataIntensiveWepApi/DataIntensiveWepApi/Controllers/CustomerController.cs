using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.DTOModels;
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
                if (!Enum.IsDefined(typeof(DataStore), db))
                    return BadRequest("Unknown database.");

                var store = (DataStore)db;
                List<CustomerDTO> customers = _customerService.GetCustomers(store);

                return Ok(customers);
            }
            catch (Exception e)
            {

                return StatusCode(500, e);
            }
        }
    }
}
