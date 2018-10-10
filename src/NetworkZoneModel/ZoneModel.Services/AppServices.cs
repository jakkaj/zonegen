﻿using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
                
                .BuildServiceProvider();

            Services = serviceProvider;

            
        }
    }
}
