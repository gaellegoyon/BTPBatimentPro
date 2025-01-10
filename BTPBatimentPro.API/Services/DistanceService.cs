using Newtonsoft.Json;


namespace BTPBatimentPro.API.Services
{
    public class DistanceService
    {
        private readonly HttpClient _httpClient;
        private const string OsrmApiUrl = "http://router.project-osrm.org/route/v1/driving/";

        public DistanceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Méthode pour calculer la distance entre l'entreprise et un chantier
        public async Task<double?> GetDistanceAsync(string startAddress, string endAddress)
        {
            // Convertir les adresses en coordonnées géographiques (latitude, longitude)
            var startCoordinates = await GetCoordinates(startAddress);
            var endCoordinates = await GetCoordinates(endAddress);

            if (startCoordinates == null || endCoordinates == null)
                return null;

            // Construire l'URL pour l'appel à OSRM
            var requestUrl = $"{OsrmApiUrl}{startCoordinates[0]},{startCoordinates[1]}?destination={endCoordinates[0]},{endCoordinates[1]}&overview=false";

            // Faire l'appel à OSRM API
            var response = await _httpClient.GetStringAsync(requestUrl);
            var route = JsonConvert.DeserializeObject<dynamic>(response);

            if (route.routes != null && route.routes.Count > 0)
            {
                // Retourner la distance en mètres
                return route.routes[0].legs[0].distance / 1000; // Convertir en kilomètres
            }

            return null;
        }

        // Méthode pour obtenir les coordonnées géographiques d'une adresse
        private async Task<double[]> GetCoordinates(string address)
        {
            var geocodeUrl = $"http://nominatim.openstreetmap.org/search?format=json&q={address}";
            var response = await _httpClient.GetStringAsync(geocodeUrl);
            var result = JsonConvert.DeserializeObject<dynamic>(response);

            if (result.Count > 0)
            {
                double lat = result[0].lat;
                double lon = result[0].lon;
                return new double[] { lat, lon };
            }

            return null;
        }
    }
}
