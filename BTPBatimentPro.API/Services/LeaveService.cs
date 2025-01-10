using BTPBatimentPro.API.Data;
using BTPBatimentPro.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BTPBatimentPro.API.Services
{
    public class LeaveService
    {
        private readonly ApplicationDbContext _context;

        public LeaveService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Leave>> GetAllAsync()
        {
            return await _context.Leaves.ToListAsync();
        }

        // Soumettre une demande de congé
        public async Task<Leave> SubmitLeaveRequestAsync(Leave leave)
        {
            _context.Leaves.Add(leave);
            await _context.SaveChangesAsync();
            return leave;
        }

        // Récupérer les congés pour validation (admin)
        public async Task<List<Leave>> GetLeaveRequestsForValidationAsync()
        {
            return await _context.Leaves.Where(l => l.Status == "Pending").ToListAsync();
        }

        // Valider ou rejeter une demande de congé
        public async Task<bool> UpdateLeaveStatusAsync(int id, string status)
        {
            var leave = await _context.Leaves.FindAsync(id);
            if (leave == null)
            {
                return false;
            }

            leave.Status = status;
            _context.Entry(leave).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        // delete
        public async Task<bool> DeleteLeaveRequestAsync(int id)
        {
            var leave = await _context.Leaves.FindAsync(id);
            if (leave == null)
            {
                return false;
            }

            _context.Leaves.Remove(leave);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
