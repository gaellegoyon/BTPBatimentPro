using BTPBatimentPro.API.Models;
using BTPBatimentPro.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BTPBatimentPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _service;

        public ProjectController(ProjectService service)
        {
            _service = service;
        }

        // GET: api/project
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var projects = await _service.GetAllProjectsAsync();
            return Ok(projects);
        }

        // GET: api/project/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _service.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        // POST: api/project
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            var newProject = await _service.CreateProjectAsync(project);
            return CreatedAtAction(nameof(GetProject), new { id = newProject.Id }, newProject);
        }

        // PUT: api/project/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            var updatedProject = await _service.UpdateProjectAsync(project);
            if (updatedProject == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/project/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var deleted = await _service.DeleteProjectAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

      
        [HttpPost("{projectId}/assign")]
        public async Task<IActionResult> AssignEmployeesToProjectAsync(int projectId, [FromBody] List<int> employeeIds)
        {
            try
            {
                await _service.AssignEmployeesToProjectAsync(projectId, employeeIds);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de l'affectation des employés : {ex.Message}");
            }
        }

        // GET: api/projects/{projectId}/employees
        [HttpGet("{projectId}/employees")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesByProject(int projectId)
        {
            var employees = await _service.GetEmployeesByProjectIdAsync(projectId);

            if (employees == null || !employees.Any())
            {
                return NotFound(new { message = "No employees found for this project." });
            }

            return Ok(employees);
        }

    }
}
