using Kolibri.Api.Extensions;

WebApplication
    .CreateBuilder(args)
    .RegisterServices()
    .Build()
    .ConfigureApplication()
    .Run();
