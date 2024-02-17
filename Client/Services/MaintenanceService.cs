using System.Net;
using Shared.ApiModels;
using System.Net.Http.Json;
using static System.Array;

namespace Client.Services
{
    public interface IMaintenanceService
    {
        Task<Maintenance[]> GetMaintenances();
        Task<Maintenance[]> GetLateMaintenances();
        Task<Maintenance> GetMaintenance(int id);
        Task CreateMaintenance(Maintenance maintenance);
    }

    public class MaintenanceService(HttpClient httpClient) : IMaintenanceService
    {
        private const string RequestUri = "https://localhost:7048/api/Maintenance";
        private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        public async Task<Maintenance[]> GetMaintenances()
        {
            var httpResponse = await _httpClient.GetAsync(RequestUri);
            if (httpResponse.StatusCode == HttpStatusCode.NoContent) return Empty<Maintenance>();
            return (await httpResponse.Content.ReadFromJsonAsync<Maintenance[]>())!;
        }

        public async Task<Maintenance[]> GetLateMaintenances()
        {
            var httpResponse = await _httpClient.GetAsync($"{RequestUri}/late-maintenance");
            if (httpResponse.StatusCode == HttpStatusCode.NoContent) return Empty<Maintenance>();
            return (await httpResponse.Content.ReadFromJsonAsync<Maintenance[]>())!;
        }

        public async Task<Maintenance> GetMaintenance(int id)
        {
            var httpResponse = await _httpClient.GetAsync($"{RequestUri}/{id}");
            if (httpResponse.StatusCode == HttpStatusCode.NotFound) return null!;
            return (await httpResponse.Content.ReadFromJsonAsync<Maintenance>())!;
        }

        public async Task CreateMaintenance(Maintenance maintenance)
        {
            await _httpClient.PostAsJsonAsync(RequestUri, maintenance);
        }
    }
}