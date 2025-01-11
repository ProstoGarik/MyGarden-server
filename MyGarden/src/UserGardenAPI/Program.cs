using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddEnvironmentVariables();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();
RegisterDataSources(builder.Services);
RegisterCoreServices(builder.Services);

var application = builder.Build();

application.MapControllers();
application.MapHealthChecks("/health");
application.UseCors();


application.Run();


void RegisterDataSources(IServiceCollection services)
{
    var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
    var dbName = Environment.GetEnvironmentVariable("POSTGRES_DB");
    var dbUser = Environment.GetEnvironmentVariable("POSTGRES_USER");
    var dbPassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
    var connectionString = $"Server={dbHost};Port=5432;Database={dbName};User Id={dbUser};Password={dbPassword};";
    services.AddSingleton<IMongoClient>(new MongoClient(connectionString));
}

void RegisterCoreServices(IServiceCollection services)
{
    services.AddScoped<IGardenRepository, GardenRepository>();
}