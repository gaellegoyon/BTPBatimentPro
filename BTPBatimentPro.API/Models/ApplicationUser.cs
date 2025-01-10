using BTPBatimentPro.API.Models;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
}
