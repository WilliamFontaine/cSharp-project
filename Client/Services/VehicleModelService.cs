using System.Net;
using System.Net.Http.Json;
using static System.Array;
using VehicleModel = Shared.ApiModels.VehicleModel;

namespace Client.Services;

public interface IVehicleModelService
{
    Task<VehicleModel[]> GetVehicleModels();
    Task<VehicleModel> GetVehicleModel(int id);
    Task CreateVehicleModel(VehicleModel vehicleModel);
    Task UpdateVehicleModel(int id, VehicleModel vehicleModel);
    Task DeleteVehicleModel(int id);
}

public class VehicleModelService(HttpClient httpClient) : IVehicleModelService
{
    private const string RequestUri = "https://localhost:7048/api/VehicleModel";
    private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public async Task<VehicleModel[]> GetVehicleModels()
    {
        var httpResponse = await _httpClient.GetAsync(RequestUri);
        if (httpResponse.StatusCode == HttpStatusCode.NoContent) return Empty<VehicleModel>();
        return (await httpResponse.Content.ReadFromJsonAsync<VehicleModel[]>())!;
    }

    public async Task<VehicleModel> GetVehicleModel(int id)
    {
        var httpResponse = await _httpClient.GetAsync($"{RequestUri}/{id}");
        if (httpResponse.StatusCode == HttpStatusCode.NotFound) return null!;
        return (await httpResponse.Content.ReadFromJsonAsync<VehicleModel>())!;
    }

    public async Task CreateVehicleModel(VehicleModel vehicleModel)
    {
        await _httpClient.PostAsJsonAsync(RequestUri, vehicleModel);
    }

    public async Task UpdateVehicleModel(int id, VehicleModel vehicleModel)
    {
        await _httpClient.PutAsJsonAsync($"{RequestUri}/{id}", vehicleModel);
    }

    public async Task DeleteVehicleModel(int id)
    {
        await _httpClient.DeleteAsync($"{RequestUri}/{id}");
    }
}