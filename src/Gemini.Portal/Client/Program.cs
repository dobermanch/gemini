using Gemini.Portal.Client;
using Gemini.Portal.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

builder.Services.AddScoped<ITwinSchemaService, TwinSchemaService>();
builder.Services.AddScoped<ITwinInterfaceService, TwinInterfaceService>();
builder.Services.AddScoped<ITwinTelemetryService, TwinTelemetryService>();
builder.Services.AddScoped<ITwinPropertyService, TwinPropertyService>();

await builder.Build().RunAsync();
