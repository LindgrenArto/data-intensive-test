using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.DTOModels;
using DataIntensiveWepApi.Models;
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

        [HttpPut("{db}")]
        public IActionResult UpdateMeasurement([FromRoute] int db, [FromBody] MeasurementDTO measurement)
        {
            try
            {
                if (!Enum.IsDefined(typeof(DataStore), db))
                    return BadRequest("Unknown database.");

                var store = (DataStore)db;
                MeasurementDTO existing = _measurementService.GetMeasurementByUuid(store, measurement.MeasurementUuid);

                if (existing == null)
                    return NotFound("Measurement is not found.");

                MeasurementDTO updatedMeasurement = _measurementService.UpdateMeasurement(store, measurement);

                return Ok(updatedMeasurement);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}