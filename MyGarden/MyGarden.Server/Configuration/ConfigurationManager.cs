using MyGarden.Server.Configuration.Repository;

namespace MyGarden.Server.Configuration
{
    public class ConfigurationManager(IConfiguration configuration)
    {
        public DataConfiguration DataConfiguration { get; } = new DataConfiguration(configuration);
    }
}
