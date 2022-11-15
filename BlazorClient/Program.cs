using BlazorClient;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Models;
using Services.APIservices;
using Services.GamesServices.Battleships;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(Constants.ServerURL) });
builder.Services.AddScoped<ApiDBService, ApiUsersDBService>();
builder.Services.AddScoped<BattleshipService, BattleshipGameLogic>();


await builder.Build().RunAsync();
