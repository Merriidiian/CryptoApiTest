using CryptoTestApi;
using CryptoTestApi.Application;
using CryptoTestApi.Middlewares;
using CryptoTestApi.Swagger;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseRouting();
app.UseEndpoints(
    endpoints => { endpoints.MapControllers(); }
);

await app.RunAsync();