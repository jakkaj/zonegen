using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ZoneModel.Model;
using ZoneModel.Services.Contracts;
using ZoneModel.Services.Options;

namespace ZoneModel.Services.Handlers
{
    public class ParseHandler : IVerbHandler<ParseZoneModelOptions>
    {
        private IZoneModelParser _parser;
        private IFileWriter _fileWriter;
        private readonly IGraphWriter _graphWriter;

        public ParseHandler(IZoneModelParser parser, IFileWriter fileWriter, IGraphWriter graphWriter)
        {
            _parser = parser;
            _fileWriter = fileWriter;
            _graphWriter = graphWriter;
        }
        public bool CanHandle(OptionResultBase command)
        {
            return (command as ParseZoneModelOptions) != null;
        }
        public async Task Handle(ParseZoneModelOptions command)
        {
            var model = _parser.Parse(command.ZoneGroup, command.Region, command.Environment, command.Directory);

            if(command.StrongType)
            {
                Console.WriteLine("Strongly Typing Ids");
                // strongly type the names in the model
                model.ToStrongIds();
            }

            if (command.WriteToFile)
            {
                var path = Path.Join(Directory.GetCurrentDirectory(), "zone-variables.yaml");
                Console.WriteLine($"Creating {path}");
                await _fileWriter.WriteAsYaml(model, path, replace: true);
            }

            if (command.Graph)
            {
                var path = Path.Join(Directory.GetCurrentDirectory(), "zone.graph");
                Console.WriteLine($"Creating {path}");
                _graphWriter.WriteGraph(model, path);
            }
        }
    }
}
