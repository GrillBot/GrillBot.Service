using Azure.Storage.Blobs;
using FileService.Actions;
using FileService.Cache;
using FileService.Factory;
using GrillBot.Core;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 1073741824; // 1GB
    options.AddServerHeader = false;
});

builder.Services.AddScoped<StorageCacheManager>();
builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddControllers(c => c.RegisterCoreFilter());
builder.Services.AddDiagnostic();
builder.Services.AddFakeDiscordClient(ServiceLifetime.Singleton);
builder.Services.AddCoreManagers();
builder.Services.AddActions();
builder.Services.AddScoped<BlobContainerFactory>();
builder.Services.AddScoped<BlobContainerClient>(provider => provider.GetRequiredService<BlobContainerFactory>().CreateClient());

builder.Services.Configure<RouteOptions>(opt => opt.LowercaseUrls = true);
builder.Services.Configure<ForwardedHeadersOptions>(opt => opt.ForwardedHeaders = ForwardedHeaders.All);

var app = builder.Build();

app.Use((context, next) =>
{
    context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
    context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");

    return next();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
