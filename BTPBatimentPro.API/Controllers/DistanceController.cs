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
        public async Task<IActionResult> GetDistance([FromQuery] string startAddress, [FromQuery] string endAddress)
        {
            if (string.IsNullOrEmpty(startAddress) || string.IsNullOrEmpty(endAddress))
            {
                return BadRequest("Both start and end addresses must be provided.");
            }

            var distance = await _distanceService.GetDistanceAsync(startAddress, endAddress);

            if (distance == null)
            {
                return NotFound("Could not calculate the distance.");
            }

            return Ok(new { distanceInKm = distance });
        }
    }
}
