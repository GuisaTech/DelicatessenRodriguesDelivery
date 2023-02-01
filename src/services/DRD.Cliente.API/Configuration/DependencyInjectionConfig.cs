using DRD.Cliente.API.Application.Commands;
using DRD.Cliente.API.Application.Events;
using DRD.Cliente.API.Data;
using DRD.Cliente.API.Data.Repository;
using DRD.Cliente.API.Models;
using DRD.Core.Mediator;
using FluentValidation.Results;
using MediatR;

namespace DRD.Cliente.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<RegistrarClienteCommand, ValidationResult>, ClienteCommandHandler>();

            services.AddScoped<INotificationHandler<ClienteRegistradoEvent>, ClienteEventHandler>();

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ClientesContext>();

            return services;
        }
    }
}
