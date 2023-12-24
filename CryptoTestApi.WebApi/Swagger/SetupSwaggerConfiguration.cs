using Microsoft.OpenApi.Models;

namespace CryptoTestApi.Swagger;

public static class SetupSwaggerConfiguration
{
    public static void SetupSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(
            options =>
            {
                options.EnableAnnotations();
                options.CustomSchemaIds(SwaggerNameProvider.GetSwaggerDisplayedName);
                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "CRYPTO API",
                        Description = "by Serdyukov Ilya"
                    }
                );
            });
    }

    public static void SetupLowercaseAll(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<RouteOptions>(
            options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            }
        );
    }
}