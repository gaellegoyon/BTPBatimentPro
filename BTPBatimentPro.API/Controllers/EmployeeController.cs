using BTPBatimentPro.API.Models;
using BTPBatimentPro.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BTPBatimentPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeesController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employees = await _employeeService.GetEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            var createdEmployee = await _employeeService.AddEmployeeAsync(employee);
            return CreatedAtAction(nameof(GetEmployees), new { id = createdEmployee.Id }, createdEmployee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }

        // POST: api/employees/{id}/attendance
        [HttpPost("{id}/attendance")]
        public async Task<ActionResult<Attendance>> RegisterAttendance(int id, Attendance attendance)
        {
            if (attendance == null)
            {
                return BadRequest("Attendance data is required.");
            }

            var registeredAttendance = await _employeeService.RegisterAttendanceAsync(id, attendance);
            if (registeredAttendance == null)
            {
                return NotFound("Employee not found.");
            }

            return CreatedAtAction(nameof(GetAttendances), new { id = registeredAttendance.Id }, registeredAttendance);
        }

        [HttpGet("{id}/attendance")]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAttendances(int id)
        {
            var attendances = await _employeeService.GetAttendancesAsync(id);
            return Ok(attendances);
        }
    }
}
