using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ZoneModel.Model;
using ZoneModel.Model.Validation;
using ZoneModel.Services;
using ZoneModel.Services.Contracts;
using ZoneModel.Services.Options;

namespace NetworkZoneModelCli
{
    class Program
    {
        private static ServiceProvider Services;
        static async Task Main(string[] args)
        {
            Services = AppServices.Services;

            var optsParser = Resolve<IOptionsParser>();

            var opts = optsParser.ParseArgs(args);

            if (opts.Item1 == ParseType.Error)
            {
                Console.WriteLine("Options error, exiting");
                Environment.Exit(1);
                return;
            }

            if (opts.Item1 == ParseType.ZoneModel)
            {
                var zoneParser = Resolve<IZoneModelParser>();

                var model = await zoneParser.Parse(opts.Item3, opts.Item4, opts.Item5, opts.Item2);
            }
            

               

            
            Console.ReadKey();
        }

        public static T Resolve<T>()
        {
            return Services.GetService<T>();
        }
    }
}
