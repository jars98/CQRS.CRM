using CQRS.Web.API.Application.Handlers;
using CQRS.Web.API.Application.Validator;
using CQRS.Web.API.Infrastructure.DataAccess.Repositories;
using CQRS.Web.API.Infrastructure.DataAccess;
using CQRS.Web.API.Infrastructure.Services.Contracts;
using CQRS.Web.API.Infrastructure.Services;
using CQRS.Web.API.Utilidad;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace CQRS.Web.API.Infrastructure.Configuration
{
    public static class Dependencias
    {
            public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
            {
                // Configuración de bases de datos
                services.AddDbContext<ApplicationDbContextPostgresSQL>(options =>
                    options.UseNpgsql(configuration.GetConnectionString("PostgresConnection")));

                services.AddSingleton<IMongoClient>(sp =>
                    new MongoClient(configuration.GetConnectionString("MongoDbConnection")));

                services.AddScoped<IMongoDatabase>(sp =>
                {
                    var client = sp.GetRequiredService<IMongoClient>();
                    var databaseName = configuration["MongoDbSettings:DatabaseName"];
                    return client.GetDatabase(databaseName);
                });

                // Repositorios
                services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
                services.AddScoped<IPropuestaRepository, PropuestaRepository>();

                // Validadores (Fluent Validation)
                services.AddValidatorsFromAssemblyContaining<UpdatePropuestaCommandValidator>();

                // AutoMapper
                services.AddAutoMapper(typeof(AutoMapperProfile));

                // MediatR
                services.AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssembly(typeof(CreatePropuestaHandler).Assembly);
                });

                return services;
            }
        }
}
