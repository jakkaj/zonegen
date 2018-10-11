using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZoneModel.Services.Contracts;
using ZoneModel.Services.Options;
using ZoneModel.Services.Utils;
using ZoneModel.Services.Zones;

namespace ZoneModel.Services
{
    public static class AppServices
    {
        public static ServiceProvider Services { get; set; }
        static AppServices()
        {
            var builder = new ConfigurationBuilder();
            // tell the builder to look for the appsettings.json file
            //builder
             //   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

           

            var Configuration = builder.Build();

            var services = new ServiceCollection();

            var serviceProvider = services
                .AddSingleton<IZoneModelParser, ZoneModelParser>()
                .AddSingleton<IOptionsParser, OptionsParser>()
                .AddSingleton<ISubnetCalculator, SubnetCalculator>()
                .AddSingleton<IFileWriter, FileWriter>()
                .AddTransient<ITemplateInitialiser, TemplateInitialiser>()
                .BuildServiceProvider();

            Services = serviceProvider;

            
        }
    }
}
