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

        [Option('g', "zonegroup", Required = false, HelpText = "Name of the zone group - e.g. contosoweb")]
        public string ZoneGroup { get; set; }

        [Option('w', "write", Default = false, Required =false, HelpText = "Write to file" )]
        public bool WriteToFile { get; set; }

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

    [Verb("sample", HelpText = "Generate some sample files")]
    public class SampleOptions
    {
        [Option('o', "output", Required = true, HelpText = "path to output file")]
        public string File { get; set; }
    }

    [Verb("init", HelpText = "Initialise a zone project")]
    public class InitOptions
    {
        [Option('d', "directory", Required = false, HelpText = "Create in this directory")]
        public string RootDirectory { get; set; }
        [Option('g', "group", Required = true, HelpText = "Zone Group Name")]
        public string ZoneGroup { get; set; }
        [Option('r', "region", Required = true, HelpText = "Region Name")]
        public string Region { get; set; }
        [Option('e', "environment", Required = true, HelpText = "Environment Name")]
        public string Environment { get; set; }
    }
}
