using Coodesh.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "coodesh.sqlserver";
var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "CoodeshDB";
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD") ?? "password@12345#";
var dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "1433";

var connectionString = $"Server={dbHost},{dbPort};Initial Catalog={dbName};User ID=SA;Password={dbPassword};MultipleActiveResultSets=true;TrustServerCertificate=true";

builder.Services.AddDbContext<ProductContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

try
{
    using IServiceScope dbServiceScope = app.Services.CreateScope();
    ProductContext? ctx = dbServiceScope.ServiceProvider.GetService<ProductContext>() ?? null;
    if (ctx is not null)
    {
        ctx.Database.Migrate();

        if (!ctx.Products.Any())
        {
            //SEEDER?
        }

    }
}
catch (Exception e)
{

}
    // Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
