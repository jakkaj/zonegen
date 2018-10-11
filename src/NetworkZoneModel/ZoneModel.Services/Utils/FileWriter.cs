using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using ZoneModel.Model;
using ZoneModel.Services.Contracts;

namespace ZoneModel.Services.Utils
{
    public class FileWriter : IFileWriter
    {
        public async Task WriteAsYaml<T>(T data, string path, bool replace = false) where T : class
        {
            var serializer = new SerializerBuilder().WithNamingConvention(new CamelCaseNamingConvention()).Build();
            var yaml = serializer.Serialize(data);
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
