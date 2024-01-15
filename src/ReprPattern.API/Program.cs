using ReprPattern.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration();
builder.Services.AddDependencyConfiguration();

var app = builder.Build();

app.UseApiConfiguration();

app.Run();