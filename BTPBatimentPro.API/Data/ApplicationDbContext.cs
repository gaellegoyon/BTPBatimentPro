using Microsoft.EntityFrameworkCore;
using BTPBatimentPro.API.Models;

namespace BTPBatimentPro.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Leave> Leaves { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Assignment>()
                .HasKey(a => new { a.EmployeeId, a.ProjectId });

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Employee)
                .WithMany()
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Project)
                .WithMany()
                .HasForeignKey(a => a.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
