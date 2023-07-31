using Coodesh.Infrastructure.Persistence.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Coodesh.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection services)
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "coodesh.sqlserver";
            var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "CoodeshDB";
            var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD") ?? "password@12345#";
            var dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "1433";

            var connectionString = $"Server={dbHost},{dbPort};Initial Catalog={dbName};User ID=SA;Password={dbPassword};MultipleActiveResultSets=true;TrustServerCertificate=true";

            services.AddDbContext<TransactionContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            return services;
        }
    }
}
