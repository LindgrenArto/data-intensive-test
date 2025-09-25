using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.DTOModels;
using DataIntensiveWepApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataIntensiveWepApi.Controllers
{
    [Route("api/measurement")]
    [ApiController]
    public class MeasurementController : Controller
    {
        private readonly IMeasurementService _measurementService;

        public MeasurementController(IMeasurementService measurementService)
        {
            _measurementService = measurementService;
        }

        [HttpGet("{db}")]
        public IActionResult GetMeasurements([FromRoute] int db)
        {
            try
            {
                if (!Enum.IsDefined(typeof(DataStore), db))
                    return BadRequest("Unknown database.");

                var store = (DataStore)db;
                List<MeasurementDTO> measurements = _measurementService.GetMeasurements(store);
                return Ok(measurements);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}