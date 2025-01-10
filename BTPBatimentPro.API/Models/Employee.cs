using System.ComponentModel.DataAnnotations;

namespace BTPBatimentPro.API.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; }
        public ICollection<Assignment> Assignments { get; set; }

    }
}
