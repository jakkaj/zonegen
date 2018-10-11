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
                System.Environment.Exit(1);
                return;
            }

            if (opts.Item1 == ParseType.ZoneModel)
            {
                var zoneParser = Resolve<IZoneModelParser>();

                var model = await zoneParser.Parse(opts.Item3, opts.Item4, opts.Item5, opts.Item2);

                if(opts.Item6)
                {
                    var writer = Resolve<IFileWriter>();
                    var path = Path.Join(Directory.GetCurrentDirectory(), "zone-variables.yaml");
                    Console.WriteLine($"Creating {path}");
                    await writer.WriteAsYaml(model, path);
                }
            }

            if(opts.Item1 == ParseType.Sample)
            {
                var calculator = Resolve<ISubnetCalculator>();
                var writer = Resolve<IFileWriter>();
                var model = Samples.RootModelSample(calculator);
                await writer.WriteAsYaml(model, opts.Item2, replace: true);
            }

            if (opts.Item1 == ParseType.Init)
            {
                var initialiser = Resolve<ITemplateInitialiser>();
                var rootDir = opts.Item2;
                var zoneGroup = opts.Item3;
                var region = opts.Item4;
                var env = opts.Item5;

                rootDir = initialiser.CreateDirectoryStructure(rootDir, zoneGroup, region, env);
                await initialiser.WriteConfigFile();
                await initialiser.WriteRuleFile("rule1");
                await initialiser.WriteZonesFile("zone1");
            }





            Console.ReadKey();
        }

        public static T Resolve<T>()
        {
            return Services.GetService<T>();
        }
    }
}
