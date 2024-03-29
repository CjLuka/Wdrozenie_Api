using ClientBlazorApp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<ClientBlazorApp.ClientRabbit>();
builder.Services.AddScoped<TestClientRabbit>();
builder.Services.AddSingleton<ClientRabbit.ConfigClientRabbit>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7293" )});
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5221") });
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
