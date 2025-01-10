using BTPBatimentPro.API.Models;
using BTPBatimentPro.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BTPBatimentPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly LeaveService _service;

        public LeaveController(LeaveService service)
        {
            _service = service;
        }

        // POST: api/leaves
        [HttpPost]
        public async Task<ActionResult<Leave>> SubmitLeaveRequest(Leave leave)
        {
            var submittedLeave = await _service.SubmitLeaveRequestAsync(leave);
            return CreatedAtAction(nameof(GetLeaveRequest), new { id = submittedLeave.Id }, submittedLeave);
        }

        // GET: api/leaves
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Leave>>> GetLeaveRequests()
        {
            var leaveRequests = await _service.GetAllAsync();
            return Ok(leaveRequests);
        }

        // PUT: api/leaves/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLeaveStatus(int id, [FromBody] string status)
        {
            var success = await _service.UpdateLeaveStatusAsync(id, status);

            if (!success)
            {
                return NotFound(new { message = "Leave request not found." });
            }

            return NoContent();
        }
        // Optionnel: un GET pour obtenir les détails d'une demande de congé
        [HttpGet("{id}")]
        public async Task<ActionResult<Leave>> GetLeaveRequest(int id)
        {
            var leaveRequests = await _service.GetLeaveRequestsForValidationAsync();
            var leave = leaveRequests.FirstOrDefault(l => l.Id == id);

            if (leave == null)
            {
                return NotFound();
            }

            return Ok(leave);
        }

        // GET: api/leaves/validation
        [HttpGet("validation")]
        public async Task<ActionResult<IEnumerable<Leave>>> GetLeaveRequestsForAdminValidation()
        {
            var leaveRequests = await _service.GetLeaveRequestsForValidationAsync();
            return Ok(leaveRequests);
        }

        // DELETE: api/leaves/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveRequest(int id)
        {
            var success = await _service.DeleteLeaveRequestAsync(id);

            if (!success)
            {
                return NotFound(new { message = "Leave request not found." });
            }

            return NoContent();
        }
    }
}
