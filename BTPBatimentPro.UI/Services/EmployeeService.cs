public class EmployeeService
{
    private readonly HttpClient _httpClient;

    public EmployeeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Employee>> GetEmployeesAsync()
    {
        var employees = await _httpClient.GetFromJsonAsync<List<Employee>>("http://localhost:5148/api/employees");
        return employees ?? new List<Employee>();
    }

    public async Task<Employee> GetEmployeeByIdAsync(int id)
    {
        var employee = await _httpClient.GetFromJsonAsync<Employee>($"http://localhost:5148/api/employees/{id}");
        if (employee == null)
        {
            throw new Exception($"Employee with id {id} not found.");
        }
        return employee;
    }

    public async Task AddEmployeeAsync(Employee employee)
    {
        await _httpClient.PostAsJsonAsync("http://localhost:5148/api/employees", employee);
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        await _httpClient.DeleteAsync($"http://localhost:5148/api/employees/{id}");
    }

    public async Task<int> GetEmployeeCountAsync()
    {
        var employees = await GetEmployeesAsync();
        return employees.Count;
    }

    public async Task PostAttendanceAsync(int id, Attendance attendance)
    {
        Console.WriteLine($"Posting attendance for employee {id}");
        Console.WriteLine($"Attendance: {attendance.Date} - {attendance.Status}, {attendance.EmployeeId}");
        await _httpClient.PostAsJsonAsync($"http://localhost:5148/api/employees/{id}/attendance", attendance);
    }

      public async Task<List<Employee>> GetAllEmployeesAsync()
    {
        var employees = await _httpClient.GetFromJsonAsync<List<Employee>>("http://localhost:5148/api/employees");
        return employees ?? new List<Employee>();
    }

}