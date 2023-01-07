using ServerSide.Hubs;
using Microsoft.Net.Http.Headers;
using Models;
using Services.Database_services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DBservice, MySqlDBService>();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
    policy.WithOrigins("http://localhost:7244", "https://localhost:7244")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithHeaders(HeaderNames.ContentType)
); 

app.MapHub<SomeMultiplayerGameHub>(Consts.HubUrl.DemoButtons);
app.MapHub<BattleshipHub>(Consts.HubUrl.Battleship);
app.MapHub<MonopolyHub>(Consts.HubUrl.Monopoly);

app.UseHttpsRedirection();
 
app.UseAuthorization();

app.MapControllers();

app.Run();
