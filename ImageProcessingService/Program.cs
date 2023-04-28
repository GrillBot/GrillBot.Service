using ImageProcessingService.Core;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(opt =>
{
    opt.Limits.MaxRequestBodySize = 1073741824; // 1GB
    opt.AddServerHeader = false;
});
builder.Services.AddCoreServices(builder.Configuration);

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
