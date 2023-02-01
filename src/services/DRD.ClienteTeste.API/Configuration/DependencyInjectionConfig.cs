using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using DRD.ClienteTeste.API.Application.Commands;
using DRD.ClienteTeste.API.Application.Events;
using DRD.ClienteTeste.API.Data;
using DRD.ClienteTeste.API.Data.Repository;
using DRD.ClienteTeste.API.Models;
using DRD.Core.Mediator;


namespace DRD.ClienteTeste.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<RegistrarClienteCommand, ValidationResult>, ClienteCommandHandler>();

            services.AddScoped<INotificationHandler<ClienteRegistradoEvent>, ClienteEventHandler>();

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ClientesContext>();
        }
    }
}
