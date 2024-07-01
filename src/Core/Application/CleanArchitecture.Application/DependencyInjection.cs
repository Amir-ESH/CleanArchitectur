using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text;
using System.Threading.RateLimiting;
using Asp.Versioning;
using CleanArchitecture.Application.Common.DTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }

    public static void AddApplicationServices(this WebApplicationBuilder builder)
    {
        #region Versioning

        builder.Services.AddApiVersioning(option =>
        {
            option.AssumeDefaultVersionWhenUnspecified = true;
            option.DefaultApiVersion = new ApiVersion(1, 0);
            option.ReportApiVersions = true;
            option.ApiVersionReader = ApiVersionReader.Combine(
                 //new QueryStringApiVersionReader("api-version"),
                 //new HeaderApiVersionReader("X-Version"),
                 new UrlSegmentApiVersionReader()//,
                 /*new MediaTypeApiVersionReader("ver")*/);
        }).AddApiExplorer(option =>
        {
            option.GroupNameFormat = "'v'VVV";
            option.SubstituteApiVersionInUrl = true;
        });

        #endregion

        #region Log4Net

        builder.Logging.ClearProviders();

        builder.Logging.AddLog4Net();

        #endregion

        #region Model Validation Result

        builder.Services.AddControllers()
               .ConfigureApiBehaviorOptions(options =>
               {
                   options.InvalidModelStateResponseFactory = context =>
                   {
                       var result = new ResultDto<object>
                       {
                           Message = "One or more validation errors occurred.",
                           StatusCode = StatusCodes.Status400BadRequest,
                           Errors = new List<ResultDto<object>.ErrorDetail>()
                       };

                       var errors = context.ModelState
                                           .Where(e =>
                                           {
                                               Debug.Assert(e.Value != null, "e.Value != null");
                                               return e.Value.Errors.Count > 0;
                                           })
                                           .ToDictionary(
                                            kvp => kvp.Key,
                                            kvp =>
                                            {
                                                Debug.Assert(kvp.Value != null, "kvp.Value != null");
                                                return kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray();
                                            });

                       var traceIdentifier = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier;

                       

                       result.SetTransactionDetails(traceIdentifier, "Failed");

                       foreach (var error in errors)
                       {
                           result.SetErrorDetails("1429", 
                                                  error.Value.ToString() ?? "validation errors occurred.", 
                                                  error.Key);
                       }
                       

                       return new BadRequestObjectResult(result);
                   };
               });

        #endregion

        #region Rate Limit

        builder.Services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.AddPolicy("FixedAndQueue", httpContext =>
                                           RateLimitPartition.GetFixedWindowLimiter(
                                            partitionKey: httpContext.User.Identity
                                                ?.Name? /*Connection.RemoteIpAddress?*/
                                                .ToString(),
                                            factory: _ =>
                                                new FixedWindowRateLimiterOptions
                                                {
                                                    PermitLimit =
                                                        10, // Number Of Request Limiter
                                                    Window =
                                                        TimeSpan
                                                            .FromSeconds(10), // Request Limit In this Time Like 10 Seconds
                                                    QueueLimit =
                                                        10, // Number Of Queue
                                                    QueueProcessingOrder =
                                                        QueueProcessingOrder
                                                            .OldestFirst // Oldest Request First
                                                }));

            options.AddSlidingWindowLimiter("sliding", sOptions =>
            {
                sOptions.Window =
                    TimeSpan.FromSeconds(15);
                sOptions.SegmentsPerWindow = 3;
                sOptions.PermitLimit = 15;
            });

            options.AddTokenBucketLimiter("token", tOptions =>
            {
                tOptions.TokenLimit = 100;
                tOptions.ReplenishmentPeriod =
                    TimeSpan.FromSeconds(5);
                tOptions.TokensPerPeriod = 10;
            });

            options.AddConcurrencyLimiter("concurrency",
                                          cOptions => { cOptions.PermitLimit = 5; });

            options.AddFixedWindowLimiter("fixedIp", fOption =>
            {
                fOption.PermitLimit =
                    10; // Number Of Request Limiter
                fOption.Window =
                    TimeSpan
                        .FromSeconds(10); // Request Limit In this Time Like 10 Seconds
            });
        });

        #endregion

        builder.AddAuthenticationExtension();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.Scan(selector => selector.FromAssemblies(Assembly.GetExecutingAssembly())
                                                  .AddClasses(false)
                                                  .AsImplementedInterfaces()
                                                  .WithScopedLifetime());

        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Services.AddMediatR(mr => mr.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        builder.Services.AddControllers().AddApplicationPart(Assembly.GetExecutingAssembly());

        builder.Services.AddHttpCacheHeaders(
                                             expirationModelOptions =>
                                             {
                                                 expirationModelOptions.MaxAge = 600;
                                             },
                                             validationModelOptions =>
                                             {
                                                 validationModelOptions.MustRevalidate = true;
                                             },
                                             middlewareOptions =>
                                             {
                                                 middlewareOptions.IgnoredStatusCodes = new[] { 500 };
                                             });

        builder.Services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
        });
    }

    private static void AddAuthenticationExtension(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication("Bearer")
               .AddJwtBearer(
                             option =>
                             {
                                 option.TokenValidationParameters = new TokenValidationParameters
                                 {
                                     ValidateIssuer = true,
                                     ValidateIssuerSigningKey = true,
                                     ValidateAudience = true,
                                     ValidIssuer =
                                         builder.Configuration.GetSection("Authentication:Issuer")
                                                .Value,
                                     ValidAudience =
                                         builder.Configuration
                                                .GetSection("Authentication:Audience").Value,
                                     IssuerSigningKey = new SymmetricSecurityKey(
                                      Encoding.UTF32.GetBytes(
                                                              builder.Configuration.GetSection(
                                                               "Authentication:SecretForKey").Value!
                                                            + "Vd4bfjbRta7z4hGQfM4ACQpbq2FtEKUy3cIB2ecMcGHfBarbQwfdxjQjIbzhRGB"))
                                 };
                             });
    }
}
