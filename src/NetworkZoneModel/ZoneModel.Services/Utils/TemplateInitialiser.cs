using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ZoneModel.Model;
using ZoneModel.Services.Contracts;

namespace ZoneModel.Services.Utils
{
    public class TemplateInitialiser : ITemplateInitialiser
    {
        private IFileWriter _fileWriter;

        public string ZoneGroupDirectoryPath { get; private set; }

        public string EnvironmentDirectoryPath { get; private set; }
        public string RegionDirectoryPath { get; private set; }

        public string RuleDirectoryPath { get; private set; }

        private string _zoneGroup;
        private string _region;
        private string _environment;


        public TemplateInitialiser(IFileWriter fileWriter)
        {
            _fileWriter = fileWriter;
        }

        public string CreateDirectoryStructure(string rootDir, string zoneGroup, string region, string env)
        {
            if (string.IsNullOrEmpty(rootDir))
            {
                rootDir = Directory.GetCurrentDirectory();
            }
            if (!Directory.Exists(rootDir))
            {
                Directory.CreateDirectory(rootDir);
            }
            _zoneGroup = zoneGroup;
            _region = region;
            _environment = env;
            var templatesDir = Path.Join(rootDir, "templates");
            Console.WriteLine($"Creating ${templatesDir}");
            Directory.CreateDirectory(templatesDir);
            ZoneGroupDirectoryPath = Path.Join(templatesDir, zoneGroup);
            Console.WriteLine($"Creating ${ZoneGroupDirectoryPath}");
            Directory.CreateDirectory(ZoneGroupDirectoryPath);
            RegionDirectoryPath = Path.Join(ZoneGroupDirectoryPath, region);
            Console.WriteLine($"Creating ${RegionDirectoryPath}");
            Directory.CreateDirectory(RegionDirectoryPath);
            EnvironmentDirectoryPath = Path.Join(RegionDirectoryPath, env);
            Console.WriteLine($"Creating ${EnvironmentDirectoryPath}");
            Directory.CreateDirectory(EnvironmentDirectoryPath);
            RuleDirectoryPath = Path.Join(EnvironmentDirectoryPath, "rule");
            Console.WriteLine($"Creating ${RuleDirectoryPath}");
            Directory.CreateDirectory(RuleDirectoryPath);

            return rootDir;
        }

        public async Task WriteConfigFile()
        {
            var sampleConfig = Samples.ConfigSample(_region, _environment, "hub");
            await _fileWriter.WriteAsYaml(sampleConfig, Path.Join(ZoneGroupDirectoryPath, "config.yaml"), replace: true);
        }

        public async Task WriteZonesFile()
        {
            var zones = new List<Zone> { new Zone {  Id="frontend", Index = 1}, new Zone{ Id = "backend", Index = 2 } };
            await _fileWriter.WriteAsYaml(zones, Path.Join(EnvironmentDirectoryPath, "zones.yaml"), replace: true);
        }

        public async Task WriteRuleFiles()
        {
            var inboundRule = new NetworkRule
            {
                Id = "open-internet",
                From = "internet",
                To = "frontend",
                Ports = new List<int> { 80, 443 },
                IsBidirectional = false,
                Description = "Allow incoming web requests"
            };
            var backendRule = new NetworkRule
            {
                Id = "access-backend",
                From = "frontend",
                To = "backend",
                Ports = new List<int> { 80, 443 },
                IsBidirectional = false,
                Description = "Allow frontend to access backend"
            };
            await _fileWriter.WriteAsYaml(inboundRule, Path.Join(RuleDirectoryPath, $"{inboundRule.Id}.yaml"), replace: true);
            await _fileWriter.WriteAsYaml(backendRule, Path.Join(RuleDirectoryPath, $"{backendRule.Id}.yaml"), replace: true);

        }
    }
}
