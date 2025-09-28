using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.DTOModels;
using DataIntensiveWepApi.Models;
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
                if (!Enum.IsDefined(typeof(DataStore), db))
                    return BadRequest("Unknown database.");

                var store = (DataStore)db;
                List<DeviceDTO> devices = _deviceService.GetDevices(store);
                return Ok(devices);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPut("{db}")]
        public IActionResult UpdateDevice([FromRoute] int db, [FromBody] DeviceDTO device)
        {
            try
            {
                if (!Enum.IsDefined(typeof(DataStore), db))
                    return BadRequest("Unknown database.");

                var store = (DataStore)db;

                DeviceDTO existing = _deviceService.GetDeviceByUuid(store, device.DeviceUuid);

                if (existing == null)
                    return NotFound("Device is not found.");

                DeviceDTO updatedDevice = _deviceService.UpdateDevice(store, device);

                return Ok(updatedDevice);
            }
            catch (Exception e)
            {

                return StatusCode(500, e);
            }
        }
    }
}