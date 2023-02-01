using DRD.WebApp.MVC.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddIdentityConfiguration();

builder.AddMvcConfiguration();

builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

app.UseMvcConfiguration();

app.Run();
