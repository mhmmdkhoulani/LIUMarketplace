using Blazored.LocalStorage;
using LIUMarketplace.UI;
using LIUMarketplace.UI.Service;
using LIUMarketplace.UI.Service.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("LIUMarketPlace.Api", client =>
{
    client.BaseAddress = new Uri("https://localhost:7081");
}).AddHttpMessageHandler<AuthorizationMessageHandler>();
builder.Services.AddTransient<AuthorizationMessageHandler>();

builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticattionStateProvider>();
builder.Services.AddScoped<IAuthenticationService, HttpAuthenticaionService>();

builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("LIUMarketPlace.Api"));


builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();


await builder.Build().RunAsync();
