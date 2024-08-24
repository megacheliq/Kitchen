using Kitchen.Service.DataAccess.Abstract;
using Kitchen.Service.DataAccess.Models;
using Kitchen.Service.DataAccess.Repositories;
using NLog;
using FluentValidation;
using NLog.Web;
using System.Text.Json.Serialization;
using System.Text.Json;
using Kitchen.Service.Middlewares.Extensions;
using Kitchen.Service.Domain.Kitchen.Abstract;
using Kitchen.Service.Domain.Kitchen.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>();
var configuration = builder.Configuration;

var logger = LogManager.GetCurrentClassLogger();

try
{
    builder.Logging.ClearProviders();
    builder.Host.UseNLog(new NLogAspNetCoreOptions { RemoveLoggerFactoryFilter = false });

    builder.Services.AddHealthChecks();

    builder.Services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));
    builder.Services.AddScoped<IModuleRepository, MongoDbModuleRepository>();
    builder.Services.AddScoped<IKitchenRepository, MongoDbKitchenRepository>();
    builder.Services.AddScoped<IKitchenValidationService, KitchenValidationService>();

    builder.Services.AddMediatR(c => c
        .RegisterServicesFromAssemblyContaining<Program>()
        );

    builder.Services.AddValidatorsFromAssemblyContaining<Program>();

    builder.Services.AddControllers()
        .AddJsonOptions(opt =>
        {
            opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

    builder.Services.AddSwaggerGen(opt =>
    {
        var xmlFile = $"{typeof(Program).Assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        opt.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
    });

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin",
            policy =>
            {
                policy.WithOrigins(configuration.GetSection("CorsSettings").Get<CorsSettings>().AllowedOrigins)
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
    });

    var app = builder.Build();

    app.UseConfigPathBase(configuration);
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseRouting();
    app.UseJsonException();
    app.UseCors("AllowSpecificOrigin");

    app.MapControllers();
    app.MapHealthChecks("/healthz");

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex);
    throw;
}
finally
{
    LogManager.Shutdown();
}