using System.Text.Json;
using Application;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services
    .AddApplication()
    .AddPersistence(configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BadBroker API",
        Version = "v1",
        Description = "BadBroker API documentation"
    });
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var databaseInitializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
    await databaseInitializer.InitializeAsync();
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

app.UseSpa(spa =>
{
    spa.UseProxyToSpaDevelopmentServer("http://127.0.0.1:5173/");
});

app.Run();