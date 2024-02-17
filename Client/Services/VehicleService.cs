using System.Net.Http.Json;
namespace Client.Services
{
    public interface IVehicleService
    {
        Task<Shared.ApiModels.Vehicle[]> GetVehicles();
        Task<Shared.ApiModels.Vehicle> GetVehicle(int id);
        Task<Shared.ApiModels.Vehicle> CreateVehicle(Shared.ApiModels.Vehicle vehicle);
        Task<Shared.ApiModels.Vehicle> UpdateVehicle(int id, Shared.ApiModels.Vehicle vehicle);
        Task<Shared.ApiModels.Vehicle> DeleteVehicle(int id);
    }

    public class VehicleService : IVehicleService
    {
        private const string RequestUri = "https://localhost:7048/api/Vehicle";
        private readonly HttpClient _httpClient;

        public VehicleService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<Shared.ApiModels.Vehicle[]> GetVehicles()
        {
            return await _httpClient.GetFromJsonAsync<Shared.ApiModels.Vehicle[]>(RequestUri);
        }

        public async Task<Shared.ApiModels.Vehicle> GetVehicle(int id)
        {
            return await _httpClient.GetFromJsonAsync<Shared.ApiModels.Vehicle>($"{RequestUri}/{id}");
        }

        public async Task<Shared.ApiModels.Vehicle> CreateVehicle(Shared.ApiModels.Vehicle vehicle)
        {
            var response = await _httpClient.PostAsJsonAsync(RequestUri, vehicle);
            return await response.Content.ReadFromJsonAsync<Shared.ApiModels.Vehicle>();
        }

        public async Task<Shared.ApiModels.Vehicle> UpdateVehicle(int id, Shared.ApiModels.Vehicle vehicle)
        {
            var response = await _httpClient.PutAsJsonAsync($"{RequestUri}/{id}", vehicle);
            return await response.Content.ReadFromJsonAsync<Shared.ApiModels.Vehicle>();
        }

        public async Task<Shared.ApiModels.Vehicle> DeleteVehicle(int id)
        {
            var response = await _httpClient.DeleteAsync($"{RequestUri}/{id}");
            return await response.Content.ReadFromJsonAsync<Shared.ApiModels.Vehicle>();
        }
    }   
}
