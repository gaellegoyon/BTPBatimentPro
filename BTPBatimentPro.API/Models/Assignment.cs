namespace BTPBatimentPro.API.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public Employee Employee { get; set; }

    }
}
