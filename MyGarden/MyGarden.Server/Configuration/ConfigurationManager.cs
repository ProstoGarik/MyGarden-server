using MyGarden.Server.Configuration.Repository;

namespace MyGarden.Server.Configuration
{
    public class ConfigurationManager(IConfiguration configuration)
    {
        public DataConfiguration DataConfiguration { get; } = new DataConfiguration(configuration);
        public SecurityConfiguration SecurityConfiguration { get; } = new SecurityConfiguration(configuration);
        public IdentityConfiguration IdentityConfiguration { get; } = new IdentityConfiguration(configuration);
        public TokenConfiguration TokenConfiguration { get; } = new TokenConfiguration(configuration);
    }
}
