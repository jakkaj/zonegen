using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace ZoneModel.Services.Options
{
    [Verb("parse", HelpText = "Parse and transform the zone model files")]
    public class ParseZoneModelOptions
    {

        [Option('d', "directory", Required = false, HelpText = "Base path of templates. Blank will use current directory")]
        public string Directory { get; set; }

        [Option('z', "zonegroup", Required = false, HelpText = "Name of the zone group - e.g. contosoweb")]
        public string ZoneGroup { get; set; }

        [Option('r', "region", Required = false, HelpText = "")]
        public string Region { get; set; }

        [Option('e', "environment", Required = false, HelpText = "The environment name - e.g. dev or prod")]
        public string Environment { get; set; }
    }

    [Verb("basic", HelpText = "Some basics for general stuff and tests")]
    public class BasicOptions
    {
        [Option('f', "file", Required = false, HelpText = "")]
        public string File { get; set; }
    }
}
