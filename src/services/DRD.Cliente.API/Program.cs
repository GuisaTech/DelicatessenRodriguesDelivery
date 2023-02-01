using DRD.Cliente.API.Configuration;
using DRD.Core.Messages;
using DRD.WebAPI.Core.Identidade;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

builder.AddApiConfiguration();

//builder.AddJwtConfiguration();

builder.AddSwaggerConfiguration();

builder.Services.AddMediatR(typeof(Command));

builder.Services.RegisterServices();

var app = builder.Build();

app.UseSwaggerConfiguration();

//app.UseApiConfiguration();

app.Run();
