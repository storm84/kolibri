using Kolibri.Api.Models;
using Kolibri.Api.Repositories;

namespace Kolibri.Api.Extensions;

public static class ConfigureApplicationExtension
{
    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        SeedData(app);

        app.UseHttpsRedirection();

        app.MapControllers();

        return app;
    }

    private static void SeedData(WebApplication app)
    {
        var packageRepository = app.Services.GetRequiredService<IPackageRepository>();
        var packages = new List<Package>{
            new Package("999111111111111111", 2000, 30, 40, 50),
            new Package("999222222222222222", 2000, 300, 40, 50)
        };

        foreach (var package in packages)
        {
            packageRepository.AddPackage(package);
        }
    }
}
