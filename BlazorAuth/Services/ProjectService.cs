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
    public async Task<List<Project>> GetProjectsByEmployeeAsync(int employeeId)
    {
        var response = await _httpClient.GetAsync($"http://localhost:5148/api/project/employee/{employeeId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Project>>() ?? new List<Project>();
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

    public async Task AssignEmployeeToProjectAsync(int projectId, int employeeId)
    {
        await _httpClient.PostAsync($"http://localhost:5148/api/project/{projectId}/assign/{employeeId}", null);
    }

    public async Task<int> GetActiveProjectsCountAsync()
    {
        var projects = await GetProjectsAsync();
        return projects.Count;
    }

    public async Task<List<Employee>> GetEmployeesByProjectAsync(int projectId)
    {
        try
        {
            var employees = await _httpClient.GetFromJsonAsync<List<Employee>>($"http://localhost:5148/api/project/{projectId}/employees");
            return employees ?? new List<Employee>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Request error: {ex.Message}");
            return new List<Employee>();
        }
    }


}