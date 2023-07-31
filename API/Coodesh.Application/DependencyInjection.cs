using Coodesh.Application.Commands;
using Coodesh.Application.Queries;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Coodesh.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateTransactionCommand>());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllTransactionsQuery>());

        services.AddScoped<IValidator<CreateTransactionCommand>, CreateTransactionCommandValidator>();
        services.AddScoped<IValidator<GetAllTransactionsQuery>, GetAllTransactionsQueryValidator>();
        return services;
    }
}
