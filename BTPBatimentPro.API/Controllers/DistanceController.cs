using BTPBatimentPro.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BTPBatimentPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistanceController : ControllerBase
    {
        private readonly DistanceService _distanceService;

        public DistanceController(DistanceService distanceService)
        {
            _distanceService = distanceService;
        }

        // GET: api/distance
        [HttpGet]
        public async Task<IActionResult> GetDistance([FromQuery] string origin, [FromQuery] string destination)
        {
            if (string.IsNullOrEmpty(origin) || string.IsNullOrEmpty(destination))
            {
                return BadRequest("Both origin and destination addresses must be provided.");
            }

            var distance = await _distanceService.CalculateDistanceAsync(origin, destination);

            if (distance == null)
            {
                return NotFound("Could not calculate the distance. Please check the addresses and try again.");
            }

            return Ok(new { distanceInKm = distance });
        }
    }
}
