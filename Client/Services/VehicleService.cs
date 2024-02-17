using System.Net;
using System.Net.Http.Json;
using Shared.ApiModels;
using static System.Array;

namespace Client.Services
{
    public interface IVehicleService
    {
        Task<Vehicle[]> GetVehicles();
        Task<Vehicle> GetVehicle(int id);
        Task CreateVehicle(Vehicle vehicle);
        Task UpdateVehicle(int id, Vehicle vehicle);
        Task DeleteVehicle(int id);
    }

    public class VehicleService(HttpClient httpClient) : IVehicleService
    {
        private const string RequestUri = "https://localhost:7048/api/Vehicle";
        private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        public async Task<Vehicle[]> GetVehicles()
        {
            var httpResponse = await _httpClient.GetAsync(RequestUri);
            if (httpResponse.StatusCode == HttpStatusCode.NoContent)
            {
                return Empty<Vehicle>();
            }

            return (await httpResponse.Content.ReadFromJsonAsync<Vehicle[]>())!;
        }

        public async Task<Vehicle> GetVehicle(int id)
        {
            var httpResponse = await _httpClient.GetAsync($"{RequestUri}/{id}");
            if (httpResponse.StatusCode == HttpStatusCode.NotFound)
            {
                return null!;
            }

            return (await httpResponse.Content.ReadFromJsonAsync<Vehicle>())!;
        }

        public async Task CreateVehicle(Vehicle vehicle)
        {
            await _httpClient.PostAsJsonAsync(RequestUri, vehicle);
        }

        public async Task UpdateVehicle(int id, Vehicle vehicle)
        {
            await _httpClient.PutAsJsonAsync($"{RequestUri}/{id}", vehicle);
        }

        public async Task DeleteVehicle(int id)
        {
            await _httpClient.DeleteAsync($"{RequestUri}/{id}");
        }
    }
}