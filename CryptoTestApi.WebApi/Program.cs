using CryptoTestApi;
using CryptoTestApi.Application;
using CryptoTestApi.Middlewares;
using CryptoTestApi.Swagger;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddApiVersioning();
builder.Services.AddEndpointsApiExplorer();

builder.SetupSwagger();

builder.Services.Configure<RouteOptions>(
    options =>
    {
        options.LowercaseUrls = true;
        options.LowercaseQueryStrings = true;
    }
);
var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseForwardedHeaders(
    new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    }
);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(
        options =>
        {
            options.PreSerializeFilters.Add(
                (swaggerDoc, httpReq) => swaggerDoc.Servers = new List<OpenApiServer>
                {
                    new() { Url = $"https://{httpReq.Host.Value}/crypto/api" },
                }
            );
        }
    );
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseRouting();
app.UseEndpoints(
    endpoints => { endpoints.MapControllers(); }
);

await app.RunAsync();