using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

public class DistanceService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<DistanceService> _logger;

    public DistanceService(HttpClient httpClient, ILogger<DistanceService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<double> GetDistanceAsync(string destination)
    {
        try
        {
            var response = await _httpClient.GetAsync($"http://localhost:5148/api/Distance?origin=Lyon&destination={destination}");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"Response from API for destination {destination}: {responseContent}");

            using (JsonDocument doc = JsonDocument.Parse(responseContent))
            {
                if (doc.RootElement.TryGetProperty("distanceInKm", out JsonElement distanceElement) && distanceElement.TryGetDouble(out double distance))
                {
                    return distance;
                }
            }

            throw new Exception("Unable to parse distance from response.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error fetching distance for destination {destination}: {ex.Message}");
            throw;
        }
    }
}