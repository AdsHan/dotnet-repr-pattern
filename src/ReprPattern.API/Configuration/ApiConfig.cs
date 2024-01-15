using FastEndpoints;
using FastEndpoints.Swagger;
using ReprPattern.API.Application.Messages.Commands;
using ReprPattern.API.Application.Services;
using ReprPattern.API.Data.Repositories;

namespace ReprPattern.API.Configuration;

public static class ApiConfig
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
    {
        services.AddFastEndpoints(o => o.IncludeAbstractValidators = true).SwaggerDocument(o =>
        {
            o.DocumentSettings = s =>
            {
                s.Title = "API";
                s.Version = "v1";
            };
        });

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly);
        });

        return services;
    }

    public static WebApplication UseApiConfiguration(this WebApplication app)
    {
        app.UseFastEndpoints()
           .UseSwaggerGen();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var service = services.GetRequiredService<ProductPopulateService>();
                service.Initialize();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Ocorreu um erro na inicialização");
            }
        }

        return app;
    }
}



