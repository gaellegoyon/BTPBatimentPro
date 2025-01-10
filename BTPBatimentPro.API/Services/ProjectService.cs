using BTPBatimentPro.API.Data;
using BTPBatimentPro.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BTPBatimentPro.API.Services
{
    public class ProjectService
    {
        private readonly ApplicationDbContext _context;

        public ProjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Récupérer tous les projets
        public async Task<List<Project>> GetAllProjectsAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        // Récupérer un projet par ID
        public async Task<Project> GetProjectByIdAsync(int id)
        {
            return await _context.Projects.FindAsync(id);
        }

        // Créer un projet
        public async Task<Project> CreateProjectAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        // Mettre à jour un projet
        public async Task<Project> UpdateProjectAsync(Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return project;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(project.Id))
                {
                    return null;
                }
                throw;
            }
        }

        // Supprimer un projet
        public async Task<bool> DeleteProjectAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return false;
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

        // Vérifier si un projet existe
        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }

        // Assigner un salarié à un chantier
        public async Task<bool> AssignEmployeeToProjectAsync(int projectId, int employeeId)
        {
            var project = await _context.Projects.FindAsync(projectId);
            var employee = await _context.Employees.FindAsync(employeeId);

            if (project == null || employee == null)
            {
                return false;
            }

            // Créer une nouvelle affectation
            var assignment = new Assignment
            {
                ProjectId = projectId,
                EmployeeId = employeeId
            };

            _context.Assignments.Add(assignment);
            await _context.SaveChangesAsync();

            return true;
        }

        // Récupérer les employés affectés à un projet
        public async Task<List<Employee>> GetEmployeesByProjectIdAsync(int projectId)
        {
            // Récupérer les affectations associées au projet
            var assignments = await _context.Assignments
                .Where(a => a.ProjectId == projectId)
                .Include(a => a.Employee) // Inclure les détails de l'employé
                .ToListAsync();

            // Retourner la liste des employés affectés
            return assignments.Select(a => a.Employee).ToList();
        }
    }
}
