using DataIntensiveWepApi.DTOModels;
using DataIntensiveWepApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataIntensiveWepApi.Controllers
{
    [Route("api/device")]
    [ApiController]
    public class DeviceController : Controller
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet("{db}")]
        public IActionResult GetDevices([FromRoute] int db)
        {
            try
            {
                List<DeviceDTO> devices = _deviceService.GetDevices(db);
                return Ok(devices);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}