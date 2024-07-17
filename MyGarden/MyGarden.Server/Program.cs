using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MyGarden.Server.Configuration.Repository;
using MyGarden.Server.Data;
using MyGarden.Server.Data.Initialization;
using MyGarden.Server.Middleware;
using MyGarden.Server.Middleware.Security;
using MyGarden.Server.Middleware.Security.Requirement;
using MyGarden.Server.Service;
using MyGarden.Server.Service.Common;
using MyGarden.Server.Service.Plants;
using MyGarden.Server.Service.Security;
using ConfigurationManager = MyGarden.Server.Configuration.ConfigurationManager;

#if DEBUG
const bool IsDebugMode = true;
#else
    private const bool IsDebugMode = false;
#endif

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationManager(builder.Configuration);

RegisterCoreServices(builder.Services);
RegisterDataSources(builder.Services, configuration.DataConfiguration);
RegisterIdentityServices(builder.Services, configuration.IdentityConfiguration);
RegisterAuthorizationServices(builder.Services);
RegisterAuthenticationServices(builder.Services, configuration.TokenConfiguration);
RegisterCorsServices(builder.Services);

var application = builder.Build();

application.UseMiddleware<ExceptionHandler>();
application.UseAuthentication();
application.UseAuthorization();
application.MapControllers();

InitializeDataSources(application);

application.Run();

/// <summary>
///     Зарегистрировать сервисы идентификации.
/// </summary>
/// <param name="services">Коллекция сервисов.</param>
/// <param name="configuration">Конфигурации модуля идентификации.</param>
void RegisterIdentityServices(IServiceCollection services, IdentityConfiguration configuration)
{
    services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<IdentityContext>()
        .AddUserManager<UserManager<IdentityUser>>()
        .AddRoleManager<RoleManager<IdentityRole>>()
        .AddSignInManager<SignInManager<IdentityUser>>();

    services.Configure<IdentityOptions>(options => configuration.GetOptions());
}

/// <summary>
///     Зарегистрировать сервисы авторизации.
/// </summary>
/// <param name="services">Коллекция сервисов.</param>
void RegisterAuthorizationServices(IServiceCollection services)
{
    services.AddAuthorizationBuilder()
        .AddPolicy(
            DefaultAuthorizationRequirement.PolicyCode,
            policy => policy.Requirements.Add(new DefaultAuthorizationRequirement()));

    services.AddSingleton<IAuthorizationHandler, PolicyAuthorizationHandler<DefaultAuthorizationRequirement>>();
}

/// <summary>
///     Зарегистрировать сервисы аутентификации.
/// </summary>
/// <param name="services">Коллекция сервисов.</param>
/// <param name="configuration">Конфигурация токенов.</param>
void RegisterAuthenticationServices(IServiceCollection services, TokenConfiguration configuration)
{
    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.IncludeErrorDetails = true;
        options.TokenValidationParameters = configuration.GetValidationParameters();
    });

    services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
    });
}


/// <summary>
///     Зарегистрировать основные сервисы и контроллеры.
/// </summary>
/// <param name="services">Коллекция сервисов.</param>
void RegisterCoreServices(IServiceCollection services)
{
    services.AddScoped<TokenService>();
    services.AddScoped<SecurityService>();

    services.AddScoped<DataEntityService>();
    services.AddScoped<AccountService>();
    services.AddScoped<GroupService>();
    services.AddScoped<PlantService>();
    services.AddScoped<EventService>();
    services.AddScoped<NotificationService>();
    services.AddScoped<GrowStageServive>();
    services.AddScoped<LightNeedService>();
    services.AddScoped<WateringNeedService>();

    services.AddTransient<ConfigurationManager>();
    services.AddControllers();
}


/// <summary>
///     Зарегистрировать источники данных.
/// </summary>
/// <param name="services">Коллекция сервисов.</param>
/// <param name="configuration">Конфигурации данных.</param>
void RegisterDataSources(IServiceCollection services, DataConfiguration configuration)
{
    var dataConfiguration = configuration.GetDefaultContextConfiguration(IsDebugMode);
    var identityConfiguration = configuration.GetIdentityContextConfiguration(IsDebugMode);

    services.AddScoped(provider => new DataContext(dataConfiguration));
    services.AddScoped(provider => new IdentityContext(identityConfiguration));

    services.AddScoped<DataInitializationScript>();
    services.AddScoped<IdentityInitializationScript>();
}

/// <summary>
///     Зарегистрировать сервисы межсайтовой аутентификации.
/// </summary>
/// <param name="services">Коллекция сервисов.</param>
void RegisterCorsServices(IServiceCollection services)
{
    services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
    });
}

/// <summary>
///     Инициализировать сессии данных.
/// </summary>
/// <param name="application">Приложение.</param>
async void InitializeDataSources(WebApplication application)
{
    using var scope = application.Services.CreateScope();

    await scope.ServiceProvider.GetRequiredService<DataInitializationScript>().Run();
    await scope.ServiceProvider.GetRequiredService<IdentityInitializationScript>().Run();
}