using Asp.Versioning;
using CleanArchitecture.Application;
using CleanArchitecture.Application.Common.Extentions;
using CleanArchitecture.Infrastructure;
using Microsoft.AspNetCore.Builder;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.AddSwaggerTools();

    builder.AddApplicationServices();

    // Add API Versioning
    builder.Services.AddApiVersioning(options =>
    {
        options.ReportApiVersions = true;
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = new ApiVersion(1, 0);
    });


    builder.Services.AddControllers();

    builder.Services.AddApplicationServices();

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddInfrastructureServices(builder.Configuration);

    var app = builder.Build();

    app.AddInfrastructurePipeline();

    var development = builder.Configuration.GetSection("Development").Value?.ToLower() ?? "false";

    if (development is not "true")
        development = "false";

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment() || development is "true")
    {
        app.UseDeveloperExceptionPage();

        app.AddSwaggerTools();

        app.UseCors(x => x.WithOrigins()
                          .AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
    }
    else
    {
        app.UseCors(x => x.WithOrigins(
                                       builder.Configuration
                                              .GetSection("CrossOptions:CrossOriginHttps").Value 
                                              ?? "https://example.com",
                                       builder.Configuration
                                              .GetSection("CrossOptions:CrossOriginHttp").Value 
                                              ?? "http://example.com")
                          .AllowAnyHeader()
                          .WithMethods("PUT", "DELETE", "GET", "POST")
                          .SetPreflightMaxAge(TimeSpan.FromSeconds(300))
                          .AllowCredentials());
    }

    app.UseHsts();

    app.UseRouting();

    app.UseHttpsRedirection();

    app.UseRateLimiter();

    app.UseAuthentication();

    app.UseAuthorization();

    app.UseStaticFiles();

    app.MapControllers()
       .RequireRateLimiting("FixedAndQueue");

    app.Use(async (context, next) =>
    {
#pragma warning disable ASP0019
        context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
        context.Response.Headers.Add("X-Xss-Protection", "1");
        context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
        context.Response.Headers.Add("programming-by", "Amir-Eslamzadeh");
        context.Response.Headers.Add("programmer-phone-number", "0098-919-924-7713");
#pragma warning restore ASP0019

        context.Response.Headers.Remove("X-Powered-By");
        context.Response.Headers.Remove("x-powered-by-plesk");
        context.Response.Headers.Remove("x-forwarded-host");
        context.Response.Headers.Remove("X-Forwarded-Host");
        context.Response.Headers.Remove("Host");
        context.Response.Headers.Remove("host");
        await next();
    });

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
