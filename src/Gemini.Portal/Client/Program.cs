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

builder.Services.AddSingleton<ITwinSchemaService, TwinSchemaService>();
builder.Services.AddSingleton<ITwinInterfaceService, TwinInterfaceService>();
builder.Services.AddSingleton<ITwinTelemetryService, TwinTelemetryService>();
builder.Services.AddSingleton<ITwinPropertyService, TwinPropertyService>();

await builder.Build().RunAsync();
