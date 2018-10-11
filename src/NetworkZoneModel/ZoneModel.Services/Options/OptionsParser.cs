using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;
using ZoneModel.Services.Contracts;

namespace ZoneModel.Services.Options
{
    public enum ParseType
    {
        ZoneModel, 
        Basic,
        Sample,
        Init,
        Error
    }

    public class OptionsParser : IOptionsParser
    {
        public (ParseType, string,string,string, string, bool) ParseArgs(string[] args)
        {
            var configuredOpts = CommandLine.Parser.Default.ParseArguments<ParseZoneModelOptions, 
                    BasicOptions, 
                    SampleOptions,
                    InitOptions>(args)
                .MapResult(
                    (ParseZoneModelOptions opts) => (ParseType.ZoneModel, opts.Directory, opts.ZoneGroup, opts.Region, opts.Environment, opts.WriteToFile),
                    (BasicOptions opts) => (ParseType.Basic, opts.File, "", "", "", false),
                    (SampleOptions opts) => (ParseType.Sample, opts.File, "", "", "", false),
                    (InitOptions opts) => (ParseType.Init, opts.RootDirectory, opts.ZoneGroup, opts.Region, opts.Environment, false),
                    errs => (ParseType.Error, "", "", "", "", false));

            return configuredOpts;
        }
    }
}
