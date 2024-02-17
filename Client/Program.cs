using Blazored.LocalStorage;
using Client;
using Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddLocalization();

// Ajout du service Local storage
builder.Services.AddBlazoredLocalStorageAsSingleton();

// Add services
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IVehicleModelService, VehicleModelService>();
builder.Services.AddScoped<IMaintenanceService, MaintenanceService>();

var app = builder.Build();

var localStorageService = app.Services.GetRequiredService<ILocalStorageService>();
var culture = await localStorageService.GetItemAsStringAsync("BlazorCulture");
if (culture == null) // Si aucune valeur dans le local storage, on initialize à en-US
{
    culture = "en-US";
    await localStorageService.SetItemAsStringAsync("BlazorCulture", culture);
}
// Change la culture dans le context du thread
var cultureInfo = new CultureInfo(culture);
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;


Task task = app.RunAsync();
