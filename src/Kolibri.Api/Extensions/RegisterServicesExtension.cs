using Kolibri.Api.Mappers;
using Kolibri.Api.Repositories;

namespace Kolibri.Api.Extensions;

public static class RegisterServicesExtension
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        RegisterServicesInternal(builder.Services, builder.Configuration);
        return builder;
    }

    private static void RegisterServicesInternal(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services
            .AddSingleton<IPackageRepository, PackageRepository>()
            .AddSingleton<IPackageMapper, PackageMapper>();
    }
}
