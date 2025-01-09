using BTPBatimentPro.API.Data;
using BTPBatimentPro.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BTPBatimentPro.API.Services
{
    public class EmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        // Enregistrer un pointage (présence ou absence)
        public async Task<Attendance> RegisterAttendanceAsync(int employeeId, Attendance attendance)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null)
            {
                return null; // Si l'employé n'existe pas
            }

            // Associer l'ID de l'employé au pointage
            attendance.EmployeeId = employeeId;
            _context.Attendances.Add(attendance); // Ajouter l'attendance à la base de données
            await _context.SaveChangesAsync();

            return attendance;
        }

        //lire les pointages d'un employé
        public async Task<IEnumerable<Attendance>> GetAttendancesAsync(int employeeId)
        {
            return await _context.Attendances.Where(a => a.EmployeeId == employeeId).ToListAsync();
        }
    }
}
