using Contracts.People;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.People;
using Service.Contract.People;
using Service.People;

namespace FavouriteColour.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("X-Pagination"));
        });

    public static void ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options =>
        {
        });

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            var xmlFile = $"{typeof(Presentation.API.AssemblyReference).Assembly.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            s.IncludeXmlComments(xmlPath);
        });
    }

    public static void ConfigurePersonService(this IServiceCollection services) =>
        services.AddScoped<IPersonService, PersonService>();

    public static void ConfigurePersonRepositoryFactory(this IServiceCollection services) =>
        services.AddScoped<IPersonRepositoryFactory, PersonRepositoryFactory>();

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
    }
}
