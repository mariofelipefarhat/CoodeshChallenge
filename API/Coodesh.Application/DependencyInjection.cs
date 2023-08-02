using Coodesh.Application.Commands;
using Coodesh.Application.Interfaces;
using Coodesh.Application.Queries;
using Coodesh.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Coodesh.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateTransactionCommand>());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllTransactionsQuery>());
        services.AddScoped<ITransactionFileProcessorService, TransactionFileProcessorService>();
        services.AddScoped<IValidator<CreateTransactionCommand>, CreateTransactionCommandValidator>();
        services.AddScoped<IValidator<GetAllTransactionsQuery>, GetAllTransactionsQueryValidator>();
        return services;
    }
}
