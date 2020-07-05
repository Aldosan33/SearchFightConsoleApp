using System.IO;
using Microsoft.Extensions.Configuration;

namespace SearchFight.Common.Configuration
{
    public class ConfigurationManager
    {
        private static IConfigurationRoot _configuration;
        static ConfigurationManager()
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appconfig.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();
        }
        public static string GoogleUrl => GetSettingByKey<string>("GoogleURL");
        public static string BingUrl => GetSettingByKey<string>("BingURL");
        public static string GoogleKey => GetSettingByKey<string>("GoogleAPIKey");
        public static string GoogleCSEKey => GetSettingByKey<string>("GoogleCSEKey");
        public static string BingAPIKey => GetSettingByKey<string>("BingAPIKey");

        private static T GetSettingByKey<T>(string key, T defaultValue = default(T))
        {
            var value = _configuration.GetSection($"AppSettings:{key}").Value;
            return string.IsNullOrWhiteSpace(value) ? defaultValue : (T)(object)(value);
        }
    }
}