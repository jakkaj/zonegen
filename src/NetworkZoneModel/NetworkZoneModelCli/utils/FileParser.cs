
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace NetworkZoneModelCli
{
    public class FileParser
    {
        public static Zone ParseZoneFile (string filePath) {

            var text = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<Zone>(text);
        }

        public static NetworkRule ParseNetworkRuleFile(string path)
        {
            var text = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<NetworkRule>(text);
        }

        public static List<T> ParseAllInDir<T>(string dirPath) where T: class
        {
            var filePaths = Directory.GetFiles(dirPath);
            var results = new List<T>();

            var deserializer = new DeserializerBuilder().WithNamingConvention(new CamelCaseNamingConvention()).Build();

            foreach (var path in filePaths)
            {
                var text = File.ReadAllText(path);
                var obj = deserializer.Deserialize<T>(text);
                results.Add(obj);
            }
            return results;
        }


    }
}