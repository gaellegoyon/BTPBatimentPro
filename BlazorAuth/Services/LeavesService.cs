public class LeavesService
{
    private readonly HttpClient _httpClient;

    public LeavesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Leave>> GetLeavesAsync()
    {
        var leaves = await _httpClient.GetFromJsonAsync<List<Leave>>("http://localhost:5148/api/leave");
        return leaves ?? new List<Leave>();
    }

    public async Task<List<Leave>> GetLeavesByEmployeeAsync(int id)
    {
        var leaves = await _httpClient.GetFromJsonAsync<List<Leave>>($"http://localhost:5148/api/leave/employee/{id}");
        return leaves ?? new List<Leave>();
    }

    public async Task<List<Leave>> GetLeavesValidationAsync()
    {
        var leaves = await _httpClient.GetFromJsonAsync<List<Leave>>("http://localhost:5148/api/leave/validation");
        return leaves ?? new List<Leave>();
    }

    public async Task AddLeaveAsync(Leave leave)
    {
        await _httpClient.PostAsJsonAsync("http://localhost:5148/api/leave", leave);
    }

    public async Task UpdateLeaveStatusAsync(int id, String status)
    {
        await _httpClient.PutAsJsonAsync($"http://localhost:5148/api/leave/{id}", status);
    }

    public async Task DeleteLeaveAsync(int id)
    {
        await _httpClient.DeleteAsync($"http://localhost:5148/api/leave/{id}");
    }
}