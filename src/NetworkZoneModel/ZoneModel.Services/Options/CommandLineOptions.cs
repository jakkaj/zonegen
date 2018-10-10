using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace ZoneModel.Services.Options
{
    public class CommandLineOptions
    {
        [Option('f', "file", Required = true, HelpText = "Set file path of the zone model directory structure")]
        public string File { get; set; }
    }
}
