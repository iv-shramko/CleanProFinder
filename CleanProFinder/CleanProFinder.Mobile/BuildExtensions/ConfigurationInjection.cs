using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace CleanProFinder.Mobile.BuildExtensions;

internal static class ConfigurationInjection
{
    internal static void AddConfiguration(this ConfigurationManager configuration)
    {
        var stream = Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream("CleanProFinder.Mobile.Properties.appsettings.json");
            
        if (stream != null)
        {
            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();

            configuration.AddConfiguration(config);
        }
    }
}