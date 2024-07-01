using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
                                                               IConfiguration configuration)
    {
        var maxRetryCount = configuration.GetSection("Database:sqlServerOptionsAction:maxRetryCount").Value ?? "5";
        var maxRetryDelay = configuration.GetSection("Database:sqlServerOptionsAction:maxRetryDelay").Value ?? "20";

        services.AddDbContext<DataContext>(
                                                options =>
                                                {
                                                    options.UseSqlServer(GetConnectionString(configuration),
                                                                         sqlServerOptionsAction: sqlOptions =>
                                                                         {
                                                                             sqlOptions.EnableRetryOnFailure(
                                                                              maxRetryCount: Convert.ToInt32(maxRetryCount),
                                                                              maxRetryDelay: TimeSpan.FromSeconds(Convert.ToInt32(maxRetryDelay)),
                                                                              errorNumbersToAdd: null);
                                                                         });
                                                });

        services.AddRepositoryLifeTime();
        
        //services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddAuthentication()
                .AddBearerToken(IdentityConstants.BearerScheme);

        services.AddAuthorizationBuilder();

        return services;
    }
    
    public static void AddInfrastructurePipeline(this IApplicationBuilder app)
    {
        app.ApplicationDbContextInitialiserConfig();
    }

    public static IServiceCollection AddRepositoryLifeTime(
        this IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);

        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

        return services;
    }

    /// <summary>
    /// Get Connection String Infos from appsettings.json
    /// </summary>
    /// <param name="configuration">IConfiguration Service</param>
    /// <returns></returns>
    private static string GetConnectionString(IConfiguration configuration)
    {
        var dataSource = configuration.GetSection("Database:ConnectionString:DataSource").Value ?? ".\\MSSQLSERVER2017";
        var initialCatalog = configuration.GetSection("Database:ConnectionString:InitialCatalog").Value
                          ?? "CleanArchitecture";
        var userId = configuration.GetSection("Database:ConnectionString:UserId").Value ?? "sa";
        var password = configuration.GetSection("Database:ConnectionString:Password").Value ?? "P4ssw0rd!";
        var integratedSecurity =
            configuration.GetSection("Database:ConnectionString:IntegratedSecurity").Value ?? "True";
        var encrypt = configuration.GetSection("Database:ConnectionString:Encrypt").Value ?? "True";
        var trustServerCertificate =
            configuration.GetSection("Database:ConnectionString:TrustServerCertificate").Value ?? "True";
        var trustedConnection =
            configuration.GetSection("Database:ConnectionString:Trusted_Connection").Value ?? "False";
        var multipleActiveResultSets =
            configuration.GetSection("Database:ConnectionString:MultipleActiveResultSets").Value ?? "True";

        return $"Data Source = {dataSource}; Initial Catalog = {initialCatalog}; User Id = {userId}; " +
            $"Password={password}; Integrated Security = {integratedSecurity}; Encrypt = {encrypt}; " +
            $"TrustServerCertificate = {trustServerCertificate}; Trusted_Connection = {trustedConnection}; " +
            $"MultipleActiveResultSets = {multipleActiveResultSets};";
    }
}
