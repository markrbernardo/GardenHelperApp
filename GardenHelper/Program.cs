using GardenHelper;
using GardenHelperApp.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001/") });
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<GardenService>();
builder.Services.AddScoped<LocationService>();
builder.Services.AddScoped<PlantInformationService>();
builder.Services.AddScoped<PlantService>();
builder.Services.AddScoped<ObservationService>();
builder.Services.AddScoped<JournalEntryService>();

// Register session service
builder.Services.AddScoped<UserSessionService>();

// Build the host
var host = builder.Build();

// Initialize session BEFORE running the app
var session = host.Services.GetRequiredService<UserSessionService>();
await session.InitializeAsync();

// Run the app
await host.RunAsync();
