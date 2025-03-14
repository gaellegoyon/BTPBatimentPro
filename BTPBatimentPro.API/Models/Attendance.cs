using System.ComponentModel.DataAnnotations;

namespace BTPBatimentPro.API.Models
{
    public class Attendance
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }  // Présence ou absence
    }
}
