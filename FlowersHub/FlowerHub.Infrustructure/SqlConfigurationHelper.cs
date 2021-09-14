using System.IO;
using Microsoft.Extensions.Configuration;

namespace FlowerHub.Infrustructure
{
    public class SqlConfigurationHelper
    {
        private static readonly IConfigurationRoot _configurationRoot;
        static SqlConfigurationHelper()
        {
            _configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static string ConnectionString() => _configurationRoot.GetConnectionString("ConnectionString");

    }
}
