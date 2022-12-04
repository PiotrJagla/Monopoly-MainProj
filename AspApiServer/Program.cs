using ASPcoreServer.Hubs;
using Microsoft.Net.Http.Headers;
using Models;
using Services.Database_services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.MapHub<SomeMultiplayerGameHub>(Constants.DemoButtonsHubURL);
app.MapHub<BattleshipHub>(Constants.BattleshipHubURL);
app.MapHub<MonopolyHub>(Constants.MonopolyHubURL);

app.UseHttpsRedirection();
 
app.UseAuthorization();

app.MapControllers();

app.Run();
