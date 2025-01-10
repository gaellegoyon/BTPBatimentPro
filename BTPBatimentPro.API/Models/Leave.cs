using System.ComponentModel.DataAnnotations;

namespace BTPBatimentPro.API.Models
{
    public class Leave
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Pending";
    }
}
