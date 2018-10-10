using NetworkZoneModelCli.Validation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetworkZoneModelCli
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting...");
            var zoneGroup = "contosoweb";
            var region = "australiaeast";
            var environment = "dev";
            // probably will need to iterate in each of these subdirs
            var root = $"templates/{zoneGroup}/{region}/{environment}";
            var rulePath = root + "/rule";
            var zonePath = root + "/zone";
            var outputFilePath = "result.json";

            var rules = FileParser.ParseAllInDir<NetworkRule>(rulePath);
            var zones = FileParser.ParseAllInDir<Zone>(zonePath);

            Console.WriteLine($"Got {rules.Count} rules");
            Console.WriteLine($"Got {zones.Count} zones");

            var zoneModel = new ZoneModel() {
                ZoneGroup = zoneGroup,
                Zones = zones,
                Rules = new List<Rule>(rules)
            };

            Console.WriteLine("Produced a Network Zone Model");

            try
            {
                zoneModel.Validate();
                Console.WriteLine("Zone Model is valid");
            } 
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // write to file
            var writer = new FileWriter();
            await writer.WriteToJsonFile(zoneModel, outputFilePath, replace: true);
            Console.WriteLine($"Wrote ZoneModel to file {outputFilePath}");
            
            // write a sample
            

            Console.ReadKey();
        }
    }
}
