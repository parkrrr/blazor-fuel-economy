using FuelEconomy;
using FuelEconomy.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<LocalStorageService>();
builder.Services.AddSingleton<AppStateService>();
builder.Services.AddSingleton<VehicleService>();
builder.Services.AddSingleton<EntriesService>();
builder.Services.AddRadzenComponents();

var built = builder.Build();

// Initialize any db stuff
var appStateService = built.Services.GetRequiredService<AppStateService>();
await appStateService.InitializeIfNecessaryAsync();

await built.RunAsync();