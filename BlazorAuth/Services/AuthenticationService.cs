using System.IdentityModel.Tokens.Jwt;
using Microsoft.JSInterop;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;

public class AuthenticationService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly NavigationManager _navigationManager; // Injection de NavigationManager
    private string? _jwtToken;
    private bool? _isAuthenticated;

    // Constructeur pour injecter IJSRuntime et NavigationManager
    public AuthenticationService(IJSRuntime jsRuntime, NavigationManager navigationManager)
    {
        _jsRuntime = jsRuntime;
        _navigationManager = navigationManager;
    }

    // Récupérer le token depuis le stockage local
    public async Task<string?> GetToken()
    {
        if (_jwtToken != null)
        {
            return _jwtToken;
        }

        _jwtToken = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt");
        return _jwtToken;
    }

    // Récupérer l'ID de l'employé à partir du token JWT
    public async Task<string?> GetEmployeeId()
    {
        var token = await GetToken();
        if (string.IsNullOrEmpty(token))
        {
            return null;
        }

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var employeeIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "employeeId");
        return employeeIdClaim?.Value;
    }

    public async Task<List<string>> GetUserRoles()
    {
        var token = await GetToken();
        if (string.IsNullOrEmpty(token))
        {
            return new List<string>();
        }

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var roles = jwtToken.Claims.Where(claim => claim.Type == ClaimTypes.Role).Select(claim => claim.Value).ToList();
        return roles;
    }

    // Vérifier si l'utilisateur est authentifié
    public async Task<bool> IsAuthenticated()
    {
        if (_isAuthenticated.HasValue)
        {
            return _isAuthenticated.Value;
        }

        var token = await GetToken();

        if (string.IsNullOrEmpty(token))
        {
            _isAuthenticated = false;
            return false;
        }

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var expiry = jwtToken.ValidTo;
            _isAuthenticated = expiry > DateTime.UtcNow; // Comparaison avec l'heure actuelle
            return _isAuthenticated.Value;
        }
        catch
        {
            _isAuthenticated = false;
            return false;
        }
    }

    // Rediriger vers la page de login si non authentifié
    public async Task RedirectToLoginIfNotAuthenticated()
    {
        var isAuthenticated = await IsAuthenticated();
        if (!isAuthenticated)
        {
            var currentPath = _navigationManager.ToBaseRelativePath(_navigationManager.Uri);
            if (!currentPath.Equals("login", StringComparison.OrdinalIgnoreCase))
            {
                _navigationManager.NavigateTo("/login", true);
            }
        }
    }

    // Déconnexion de l'utilisateur
    public async Task Logout()
    {
        _jwtToken = null;
        _isAuthenticated = false;
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "jwt");
        _navigationManager.NavigateTo("/login", true); // Redirection après déconnexion
    }
}
