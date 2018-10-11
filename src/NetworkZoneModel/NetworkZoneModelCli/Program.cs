using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
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

            if (opts.ParseType == ParseType.Error)
            {
                Console.WriteLine("Options error, exiting");
                System.Environment.Exit(1);
                return;
            }

            if (opts.ParseType == ParseType.ZoneModel)
            {
                var zm = opts as ParseZoneModelOptions;
                var zoneParser = Resolve<IZoneModelParser>();

                var model = zoneParser.Parse(zm.ZoneGroup, zm.Region, zm.Environment, zm.Directory);

                if(zm.WriteToFile)
                {
                    var writer = Resolve<IFileWriter>();
                    var path = Path.Join(Directory.GetCurrentDirectory(), "zone-variables.yaml");
                    Console.WriteLine($"Creating {path}");
                    await writer.WriteAsYaml(model, path, replace: true);
                }
            }

            if(opts.ParseType == ParseType.Sample)
            {
                var zm = opts as SampleOptions;
                var calculator = Resolve<ISubnetCalculator>();
                var writer = Resolve<IFileWriter>();
                var model = Samples.RootModelSample(calculator);
                await writer.WriteAsYaml(model, zm.File, replace: true);
            }

            if (opts.ParseType == ParseType.Init)
            {
                var zm = opts as InitOptions;
                var initialiser = Resolve<ITemplateInitialiser>();

                zm.RootDirectory = initialiser.CreateDirectoryStructure(zm.RootDirectory, zm.ZoneGroup, zm.Region, zm.Environment);
                await initialiser.WriteConfigFile();
                await initialiser.WriteRuleFiles();
                await initialiser.WriteZonesFile();
            }
        }

        public static T Resolve<T>()
        {
            return Services.GetService<T>();
        }
    }
}
