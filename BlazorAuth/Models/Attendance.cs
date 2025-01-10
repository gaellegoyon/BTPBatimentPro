using System.ComponentModel.DataAnnotations;

public class Attendance
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [StringLength(20)]
    public string Status { get; set; }
}