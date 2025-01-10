using BTPBatimentPro.API.Data;
using BTPBatimentPro.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic; 
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
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

        // Assigner des employés à un projet
        public async Task AssignEmployeesToProjectAsync(int projectId, List<int> employeeIds)
        {
            var project = await _context.Projects.Include(p => p.Assignments).FirstOrDefaultAsync(p => p.Id == projectId);
            if (project == null)
            {
                throw new Exception("Projet non trouvé");
            }

            // Vérifier que la liste d'employés n'est pas vide
            if (employeeIds == null || !employeeIds.Any())
            {
                throw new ArgumentException("Aucun employé sélectionné.");
            }

            // Récupérer les employés à affecter
            var employees = await _context.Employees.Where(e => employeeIds.Contains(e.Id)).ToListAsync();
            if (employees.Count != employeeIds.Count)
            {
                throw new Exception("Certains employés n'ont pas été trouvés.");
            }

            // Ajouter les affectations pour chaque employé
            foreach (var employee in employees)
            {
                // Créer une nouvelle affectation pour l'employé et le projet
                var assignment = new Assignment
                {
                    EmployeeId = employee.Id,
                    ProjectId = projectId
                };

                // Ajouter l'affectation à la base de données
                _context.Assignments.Add(assignment);
            }

            await _context.SaveChangesAsync(); // Sauvegarder les modifications
        }

        // Récupérer les employés affectés à un projet
        public async Task<List<Employee>> GetEmployeesByProjectIdAsync(int projectId)
        {
            var employees = await _context.Assignments
                .Where(a => a.ProjectId == projectId)
                .Select(a => a.Employee)
                .ToListAsync();

            return employees ?? new List<Employee>();
        }
        
      
    }
}
