using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CleanArchitecture.Application.Common.Extentions;

public static class SwaggerExtensions
{
    public static void AddSwaggerTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {

            var provider = builder.Services.BuildServiceProvider()
                                  .GetRequiredService<IApiVersionDescriptionProvider>();
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                                   description.GroupName,
                                   new OpenApiInfo
                                   {
                                       Title = $"Clean Architecture Web API {description.ApiVersion}",
                                       Version = $"{description.ApiVersion}",
                                       Description = "This is Clean Architecture Web Application API",
                                       TermsOfService = new Uri("https://example.com/terms"),
                                       Contact = new OpenApiContact
                                       {
                                           Name = "Amir Eslamzadeh",
                                           Url = new Uri("mail:amir@eslamzadeh.com")
                                       },
                                       License = new OpenApiLicense
                                       {
                                           Name = "MIT License",
                                           Url = new Uri("https://github.com/Amir-ESH/CleanArchitectur/tree/main?tab=MIT-1-ov-file#readme")
                                       }
                                   });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                                               {
                                                   {
                                                       new OpenApiSecurityScheme
                                                       {
                                                           Reference = new OpenApiReference
                                                           {
                                                               Type = ReferenceType.SecurityScheme,
                                                               Id = "Bearer"
                                                           },
                                                           Scheme = "Bearer",
                                                           Name = "Bearer",
                                                           In = ParameterLocation.Header,

                                                       },
                                                       new List<string>()
                                                   }
                                               });
            }

            options.CustomSchemaIds(type => type.FullName);

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = """
                               JWT Authorization header using the Bearer scheme.
                               Enter " Bearer " [space] and then your Token in the text input below.
                               Example: " Bearer 12345AbCdEfGhIjKlMnOp "
                               """,
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });


        });
    }

    public static void AddSwaggerTools(this WebApplication app)
    {
        var versionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        app.UseSwagger();
        app.UseSwaggerUI(option =>
                         {
                             foreach (var description in versionDescriptionProvider.ApiVersionDescriptions)
                             {
                                 option.SwaggerEndpoint($"/swagger/{description.GroupName.ToLower()}/swagger.json", $"Clean Architecture Project Web API {description.GroupName.ToUpper()}");
                                 option.RoutePrefix = "swagger";
                                 option.DocumentTitle = "Clean Architecture Project API Test Page";
                             }
                         });
    }
}
