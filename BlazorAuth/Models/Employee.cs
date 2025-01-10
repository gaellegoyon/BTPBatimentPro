using System.ComponentModel.DataAnnotations;

public class Employee
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le prénom est requis.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Le nom est requis.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Le rôle est requis.")]
    public string Role { get; set; }
}
