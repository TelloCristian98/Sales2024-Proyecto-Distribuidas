using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Configuracion de serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/api-gateway.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

//configuracion de autentificacion JWT
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5206"; // URL de tu proveedor de identidad
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = "https://localhost:5206",
            ValidAudience = "api-gateway",
            ValidateLifetime = true
        };
    });

//Configuracion Ocelot

builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddOcelot();

//middleware de throttling
builder.Services.AddRateLimiter(_ => 
{
    _.AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 100;
        options.Window = TimeSpan.FromMinutes(1);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    });
});

//configuracion de redis para cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:5206"; // Cambia según tu instancia de Redis
    options.InstanceName = "APIGateway_";
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.Use(async (context, next) =>
{
    Log.Information("Acceso: {Method} {Path}", context.Request.Method, context.Request.Path);
    await next.Invoke();
});

app.UseRateLimiter();
app.UseMiddleware<CacheMiddleware>();
app.UseOcelot().Wait();
app.Run();


public class CacheMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IDistributedCache _cache;

    public CacheMiddleware(RequestDelegate next, IDistributedCache cache)
    {
        _next = next;
        _cache = cache;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var cacheKey = context.Request.Path.ToString();
        var cachedResponse = await _cache.GetStringAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedResponse))
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(cachedResponse);
            return;
        }

        var originalResponseBodyStream = context.Response.Body;
        using (var responseBodyStream = new MemoryStream())
        {
            context.Response.Body = responseBodyStream;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            await _cache.SetStringAsync(cacheKey, responseBody, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });

            await responseBodyStream.CopyToAsync(originalResponseBodyStream);
        }
    }
}




