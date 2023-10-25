using Microsoft.Extensions.Logging;
﻿using System.Reflection;
using CleanProFinder.Mobile.ViewModels;
using CleanProFinder.Mobile.Views;
using CleanProFinder.Mobile.Services;
using Microsoft.Extensions.Configuration;

namespace CleanProFinder.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            var stream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("CleanProFinder.Mobile.Properties.appsettings.json");
            
            if (stream != null)
            {
                var config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();

                builder.Configuration.AddConfiguration(config);
            }

#if DEBUG
            builder.Logging.AddDebug();
#endif
            
            builder.Services.AddTransient<RolePage>();
            builder.Services.AddTransient<RoleViewModel>();

            builder.Services.AddSingleton<IHttpService, HttpService>();
            return builder.Build();
        }
    }
}