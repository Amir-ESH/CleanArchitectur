using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CleanArchitecture.Application.Common.Extentions;

public class CustomDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var pathsToRemove = swaggerDoc.Paths.Select(path => path.Key).ToList();

        foreach (var path in pathsToRemove)
        {
            swaggerDoc.Paths.Remove(path);
            foreach (var apiVersion in context.ApiDescriptions.GroupBy(d => d.GroupName))
            {
                var newPath = path.Replace("{version}", apiVersion.Key);
                swaggerDoc.Paths.Add(newPath, new OpenApiPathItem());
            }
        }
    }
}
