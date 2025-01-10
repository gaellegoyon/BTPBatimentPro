using System.ComponentModel.DataAnnotations;

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
}