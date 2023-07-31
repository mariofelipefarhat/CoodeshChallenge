using ApiSketch.Application.Commands;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Coodesh.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateTransactionCommand>());
        services.AddScoped<IValidator<CreateTransactionCommand>, CreateTransactionCommandValidator>();
        return services;
    }
}
