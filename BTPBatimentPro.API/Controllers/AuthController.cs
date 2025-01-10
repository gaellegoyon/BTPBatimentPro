using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthController(ILogger<AuthController> logger, UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _logger = logger;
        _userManager = userManager;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user != null)
        {
            _logger.LogInformation("User found: {Username}", model.Username);

            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (result)
            {
                _logger.LogInformation("Password validation succeeded for user: {Username}", model.Username);

                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault() ?? "User"; // Assurez-vous que l'utilisateur a un rôle, sinon définissez-le par défaut sur "User"

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("employeeId", user.EmployeeId.ToString()),
                    new Claim(ClaimTypes.Role, role) // Ajouter le rôle de l'utilisateur
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    employeeId = user.EmployeeId, // Ajouter l'ID de l'employé à la réponse
                    role = role // Ajouter le rôle de l'utilisateur à la réponse
                });
            }
            else
            {
                _logger.LogWarning("Password validation failed for user: {Username}", model.Username);
            }
        }
        else
        {
            _logger.LogWarning("User not found: {Username}", model.Username);
        }

        return Unauthorized();
    }
}