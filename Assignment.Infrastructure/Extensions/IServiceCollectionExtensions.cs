using Assignment.Application.Interfaces;
using Assignment.Domain.Common.Interfaces;
using Assignment.Infrastructure.Services;
using Assignment.Domain.Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment.Infrastructure.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddServices();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services
        .AddTransient<IMediator, Mediator>()
            .AddTransient<IDomainEventDispatcher, DomainEventDispatcher>()
            .AddTransient<IDateTimeService, DateTimeService>()
            .AddTransient<IEmailService, EmailService>();
    }
}
