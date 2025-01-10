using System.Text.Json;
using System.Text;
public class ProjectService
{
    private readonly HttpClient _httpClient;

    public ProjectService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Project>> GetProjectsAsync()
    {
        var projects = await _httpClient.GetFromJsonAsync<List<Project>>("http://localhost:5148/api/project");
        return projects ?? new List<Project>();
    }

    public async Task AddProjectAsync(Project project)
    {
        await _httpClient.PostAsJsonAsync("http://localhost:5148/api/project", project);
    }

    public async Task UpdateProjectAsync(int id, Project project)
    {
        await _httpClient.PutAsJsonAsync($"http://localhost:5148/api/project/{id}", project);
    }

    public async Task DeleteProjectAsync(int id)
    {
        await _httpClient.DeleteAsync($"http://localhost:5148/api/project/{id}");
    }

 
    public async Task<int> GetActiveProjectsCountAsync()
    {
        var projects = await GetProjectsAsync();
        return projects.Count;
    }

       // Récupérer les employés par projet
    public async Task<List<Employee>> GetEmployeesByProjectAsync(int projectId)
    {
        var response = await _httpClient.GetFromJsonAsync<List<Employee>>($"http://localhost:5148/api/project/{projectId}/employees");
        return response ?? new List<Employee>();
    }

    // Assigner des employés à un projet
    public async Task AssignEmployeesToProjectAsync(int projectId, List<int> employeeIds)
    {
        var content = new StringContent(JsonSerializer.Serialize(employeeIds), Encoding.UTF8, "application/json");
        await _httpClient.PostAsync($"http://localhost:5148/api/project/{projectId}/assign", content);
    }
}