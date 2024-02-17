using System.Net.Http.Json;

namespace Client.Services
{
    public interface IMaintenanceService
    {
        Task<Shared.ApiModels.Maintenance[]> GetMaintenances();
        Task<Shared.ApiModels.Maintenance[]> GetLateMaintenances();
        Task<Shared.ApiModels.Maintenance> GetMaintenance(int id);
        Task<Shared.ApiModels.Maintenance> CreateMaintenance(Shared.ApiModels.Maintenance maintenance);
    }

    public class MaintenanceService : IMaintenanceService
    {
        private const string RequestUri = "https://localhost:7048/api/Maintenance";
        private readonly HttpClient _httpClient;

        public MaintenanceService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<Shared.ApiModels.Maintenance[]> GetMaintenances()
        {
            return await _httpClient.GetFromJsonAsync < Shared.ApiModels.Maintenance[]>(RequestUri);
        }

        public async Task<Shared.ApiModels.Maintenance[]> GetLateMaintenances()
        {
            return await _httpClient.GetFromJsonAsync < Shared.ApiModels.Maintenance[]>($"{RequestUri}/late-maintenance");
        }

        public async Task<Shared.ApiModels.Maintenance> GetMaintenance(int id)
        {
            return await _httpClient.GetFromJsonAsync<Shared.ApiModels.Maintenance>($"{RequestUri}/{id}");
        }

        public async Task<Shared.ApiModels.Maintenance> CreateMaintenance(Shared.ApiModels.Maintenance maintenance)
        {
            var response = await _httpClient.PostAsJsonAsync(RequestUri, maintenance);
            return await response.Content.ReadFromJsonAsync<Shared.ApiModels.Maintenance>();
        }
    }

}
