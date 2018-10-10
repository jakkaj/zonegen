using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace NetworkZoneModelCli
{
    public class FileWriter
    {
        public async Task WriteToJsonFile(ZoneModel model, string path, bool replace = false)
        {
            var json = JsonConvert.SerializeObject(model);
            await WriteTextToFile(path, json, replace);

        }

        public async Task WriteToYamlFile(Zone zone, string path, bool replace = false)
        {
            var serializer = new SerializerBuilder().WithNamingConvention(new CamelCaseNamingConvention()).Build();
            var yaml = serializer.Serialize(zone);
            await WriteTextToFile(path, yaml, replace);
        }

        public async Task WriteToYamlFile(NetworkRule rule, string path, bool replace = false)
        {
            var serializer = new SerializerBuilder().WithNamingConvention(new CamelCaseNamingConvention()).Build();
            var yaml = serializer.Serialize(rule);
            await WriteTextToFile(path, yaml, replace);
        }

        private static async Task WriteTextToFile(string path, string json, bool replace)
        {
            if (File.Exists(path) && replace)
            {
                File.Delete(path);
                await File.WriteAllTextAsync(path, json);
            }
            else if (!File.Exists(path))
            {
                await File.WriteAllTextAsync(path, json);
            }
            else
            {
                // this is an error condition - file exists and !replace
                throw new InvalidOperationException($"File exists at {path}, but replace arg was {replace}");
            }
        }

        
    }
}
