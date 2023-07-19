using Microsoft.Extensions.Configuration;

namespace CaseProject.AppConfig
{
    public static class AppConfigurationService
    {
        private static IConfiguration _configuration;

        public static void Configure(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string GetConfiguration(string key)
        {
            return _configuration.GetValue<string>(key);
        }

        public static string GetApiMainUrl()
        {
            return GetConfiguration("ApiMainUrl");
        }
    }
}