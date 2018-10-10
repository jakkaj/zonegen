using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace ZoneModel.Services.Options
{
    public class CommandLineOptions
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }
    }
}
