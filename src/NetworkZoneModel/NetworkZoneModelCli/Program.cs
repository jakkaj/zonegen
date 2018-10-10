using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ZoneModel.Model;
using ZoneModel.Model.Validation;

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
            // sample ids
            var sampleRuleId = "ruleid";
            var sampleZoneId = "zoneid";
            var sampleOutputPath = Directory.GetCurrentDirectory();

            var rules = FileParser.ParseAllInDir<NetworkRule>(rulePath);
            var zones = FileParser.ParseAllInDir<Zone>(zonePath);

            Console.WriteLine($"Got {rules.Count} rules");
            Console.WriteLine($"Got {zones.Count} zones");

            var model = new RootModel() {
                ZoneGroup = zoneGroup,
                Zones = zones,
                Rules = new List<Rule>(rules)
            };

            Console.WriteLine("Produced a Network Zone Model");

            try
            {
                model.Validate();
                Console.WriteLine("Zone Model is valid");
            } 
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // write to file
            var writer = new FileWriter();
            await writer.WriteToJsonFile(model, outputFilePath, replace: true);
            Console.WriteLine($"Wrote ZoneModel to file {outputFilePath}");

            // make some sample classes
            var sampleRule = Samples.NetworkRuleSample(sampleRuleId);
            var sampleZone = Samples.ZoneExample(sampleZoneId);
            // write a sample

            await writer.WriteToYamlFile(sampleZone, sampleOutputPath);
            await writer.WriteToYamlFile(sampleRule, sampleOutputPath);
            Console.WriteLine("Wrote sample files");
            Console.ReadKey();
        }
    }
}
