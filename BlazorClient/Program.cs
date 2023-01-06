using BlazorClient;
using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Models;
using Services.APIservices;
using Services.GamesServices.Battleships;
using Services.GamesServices.BlackJack;
using Services.GamesServices.Monopoly;
using Services.GamesServices.TicTacToe;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(Consts.ServerURL) });
builder.Services.AddScoped<ApiDBService, ApiUsersDBService>();
builder.Services.AddScoped<BattleshipService, BattleshipGameLogic>();
builder.Services.AddScoped<BlackJackService, BlackJackGameLogic>();
builder.Services.AddScoped<TicTacToeService, TicTacToeGameLogic>();
builder.Services.AddScoped<MonopolyService, MonopolyGameLogic>();
builder.Services.AddScoped<IModalService, ModalService>();


await builder.Build().RunAsync();
