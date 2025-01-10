using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTPBatimentPro.API.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public string Address { get; set; }

        // Liste des affectations (relation avec les employés via Assignment)
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();
    }
}
