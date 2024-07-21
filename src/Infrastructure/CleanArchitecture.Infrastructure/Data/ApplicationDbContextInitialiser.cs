using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Data;

public static class ApplicationDbContextInitialiser
{
    public static void ApplicationDbContextInitialiserConfig(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        if (serviceScope.ServiceProvider.GetService<DataContext>() is { } context)
            context.Database.Migrate();

        //if (serviceScope.ServiceProvider.GetService<DataCacheContext>() is { } cacheContext)
        //    cacheContext.Database.Migrate();
    }
}