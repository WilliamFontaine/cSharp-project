using System.Net.Http.Json;

namespace Client.Services
{
    public interface IVehicleModelService
    {
        Task<Shared.ApiModels.VehicleModel[]> GetVehicleModels();
        Task<Shared.ApiModels.VehicleModel> GetVehicleModel(int id);
        Task<Shared.ApiModels.VehicleModel> CreateVehicleModel(Shared.ApiModels.VehicleModel vehicleModel);
        Task<Shared.ApiModels.VehicleModel> UpdateVehicleModel(int id, Shared.ApiModels.VehicleModel vehicleModel);
        Task<Shared.ApiModels.VehicleModel> DeleteVehicleModel(int id);
    }

    public class VehicleModelService : IVehicleModelService
    {
        private const string RequestUri = "https://localhost:7048/api/VehicleModel";
        private readonly HttpClient _httpClient;

        public VehicleModelService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<Shared.ApiModels.VehicleModel[]> GetVehicleModels()
        {
            return await _httpClient.GetFromJsonAsync < Shared.ApiModels.VehicleModel[]>(RequestUri);
        }

        public async Task<Shared.ApiModels.VehicleModel> GetVehicleModel(int id)
        {
            return await _httpClient.GetFromJsonAsync<Shared.ApiModels.VehicleModel>($"{RequestUri}/{id}");
        }

        public async Task<Shared.ApiModels.VehicleModel> CreateVehicleModel(Shared.ApiModels.VehicleModel vehicleModel)
        {
            var response = await _httpClient.PostAsJsonAsync(RequestUri, vehicleModel);
            return await response.Content.ReadFromJsonAsync<Shared.ApiModels.VehicleModel>();
        }

        public async Task<Shared.ApiModels.VehicleModel> UpdateVehicleModel(int id, Shared.ApiModels.VehicleModel vehicleModel)
        {
            var response = await _httpClient.PutAsJsonAsync($"{RequestUri}/{id}", vehicleModel);
            return await response.Content.ReadFromJsonAsync<Shared.ApiModels.VehicleModel>();
        }

        public async Task<Shared.ApiModels.VehicleModel> DeleteVehicleModel(int id)
        {
            var response = await _httpClient.DeleteAsync($"{RequestUri}/{id}");
            return await response.Content.ReadFromJsonAsync<Shared.ApiModels.VehicleModel>();
        }
    }
}
