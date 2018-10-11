using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using ZoneModel.Model;

namespace ZoneModel.Services.Utils
{
    public class FileParser
    {
        public static RootModel LoadRootModelFromConfig(string path)
        {
            var text = File.ReadAllText(path);
            var deserializer = new DeserializerBuilder().WithNamingConvention(new CamelCaseNamingConvention()).Build();
            return deserializer.Deserialize<RootModel>(text);
        }
        public static List<Zone> ParseZonesFile (string filePath) {

            var text = File.ReadAllText(filePath);
            var deserializer = new DeserializerBuilder().WithNamingConvention(new CamelCaseNamingConvention()).Build();

            return deserializer.Deserialize<List<Zone>>(text);
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