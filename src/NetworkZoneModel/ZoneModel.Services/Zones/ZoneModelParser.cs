using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoneModel.Model;
using ZoneModel.Model.Validation;
using ZoneModel.Services.Contracts;
using ZoneModel.Services.Utils;

namespace ZoneModel.Services.Zones
{
    public class ZoneModelParser : IZoneModelParser
    {
        private ISubnetCalculator _subnetCalculator;
        public ZoneModelParser(ISubnetCalculator subnetCalculator)
        {
            _subnetCalculator = subnetCalculator;
        }
        public async Task<RootModel> Parse(string zoneGroup, string region, string environment,
            string basePath = null)
        {
            if (basePath == null)
            {
                basePath = Directory.GetCurrentDirectory();
            }

            var fullPath = Path.GetFullPath(basePath);

            Console.WriteLine("Starting...");
           
            // probably will need to iterate in each of these subdirs
            //var root = Path.Join(fullPath, $"{zoneGroup}/{region}/{environment}");
            var configPath = Path.Join(fullPath, zoneGroup, "config.yaml");
            
            
            var outputFilePath = "result.json";
            // sample ids
            var sampleRuleId = "ruleid";
            var sampleZoneId = "zoneid";
            var sampleOutputPath = Directory.GetCurrentDirectory();

            var model = FileParser.LoadRootModelFromConfig(configPath);

            model.ZoneGroup = zoneGroup;
            
            // iterate regions in config
            foreach(var reg in model.Regions)
            {
                foreach(var env in reg.Environments.Where(e => ! e.Ignore))
                {
                    var zonePath = Path.Combine(fullPath, zoneGroup, reg.Id, env.Id, "zones.yaml");
                    var rulePath = Path.Combine(fullPath, zoneGroup, reg.Id, env.Id, "rule");
                    var zones = FileParser.ParseZonesFile(zonePath);
                    Console.WriteLine($"Got {zones.Count} zones");
                    zones.ForEach(z => z.Cidr = _subnetCalculator.GetSubnetOffset(env.Cidr, (byte)env.SubnetMaskSize, z.Index));
                    env.Zones = zones;

                    var rules = FileParser.ParseAllInDir<NetworkRule>(rulePath);
                    Console.WriteLine($"Got {rules.Count} rules");
                    env.Rules = new List<Rule>(rules);
                }
            }

            Console.WriteLine("Produced a Network Zone Model");

            try
            {
                model.Validate();
                Console.WriteLine("Zone Model is valid");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // write to file
            //var writer = new FileWriter();
            //await writer.WriteToJsonFile(model, outputFilePath, replace: true);
            //Console.WriteLine($"Wrote ZoneModel to file {outputFilePath}");

            //// make some sample classes
            //var sampleRule = Samples.NetworkRuleSample(sampleRuleId);
            //var sampleZone = Samples.ZoneExample(sampleZoneId);
            //// write a sample

            //await writer.WriteToYamlFile(sampleZone, sampleOutputPath);
            //await writer.WriteToYamlFile(sampleRule, sampleOutputPath);


            return model;
        }
    }
}
