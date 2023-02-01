using DRD.Catalogo.API.Configuration;
using DRD.WebAPI.Core.Identidade;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
            .SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables();

builder.AddApiConfiguration();

builder.AddJwtConfiguration();

builder.Services.AddMediatR(typeof(Program));

builder.AddSwaggerConfiguration();

builder.Services.RegisterServices();

var app = builder.Build();

app.UseSwaggerConfiguration();

app.UseApiConfiguration();

app.Run();
