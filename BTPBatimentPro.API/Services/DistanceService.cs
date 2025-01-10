using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace BTPBatimentPro.API.Services
{
    public class DistanceService
    {
        private readonly HttpClient _httpClient;
        private readonly string _googleApiKey;

        public DistanceService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _googleApiKey = configuration["GoogleApiKey"];
        }

        public async Task<double?> CalculateDistanceAsync(string origin, string destination)
        {
            try
            {
                var url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={origin}&destinations={destination}&key={_googleApiKey}";

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                using var jsonDoc = JsonDocument.Parse(content);

                var distanceInMeters = jsonDoc
                    .RootElement
                    .GetProperty("rows")[0]
                    .GetProperty("elements")[0]
                    .GetProperty("distance")
                    .GetProperty("value")
                    .GetDouble();

                // Retourner la distance en kilomètres
                return distanceInMeters / 1000.0;
            }
            catch
            {
                return null;
            }
        }
    }
}