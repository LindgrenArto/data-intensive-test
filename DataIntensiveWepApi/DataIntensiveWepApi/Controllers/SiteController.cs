using DataIntensiveWepApi.ConnectionResolver;
using DataIntensiveWepApi.DTOModels;
using DataIntensiveWepApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataIntensiveWepApi.Controllers
{
    [Route("api/site")]
    [ApiController]
    public class SiteController : Controller
    {
        private readonly ISiteService _siteService;

        public SiteController(ISiteService siteService)
        {
            _siteService = siteService;
        }

        [HttpGet("{db}")]
        public IActionResult GetSites([FromRoute] int db)
        {
            try
            {
                if (!Enum.IsDefined(typeof(DataStore), db))
                    return BadRequest("Unknown database.");

                var store = (DataStore)db;
                List<SiteDTO> sites = _siteService.GetSites(store);
                return Ok(sites);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
        [HttpPut("{db}")]
        public IActionResult UpdateSite([FromRoute] int db, [FromBody] UpdateSiteDTO site)
        {

            try
            {
                if (!Enum.IsDefined(typeof(DataStore), db))
                    return BadRequest("Unknown database.");

                var store = (DataStore)db;

                SiteDTO existing = _siteService.GetSiteByUuid(store, site.SiteUuid);

                if (existing == null)
                    return NotFound("Site is not found.");

                UpdateSiteDTO updatedSite = _siteService.UpdateSite(store, site);

                return Ok(updatedSite);
            }
            catch (Exception e)
            {

                return StatusCode(500, e);
            }
        }
    }
}