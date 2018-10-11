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
        public (ParseType, string,string,string, string) ParseArgs(string[] args)
        {
            var configuredOpts = CommandLine.Parser.Default.ParseArguments<ParseZoneModelOptions, 
                    BasicOptions, 
                    SampleOptions,
                    InitOptions>(args)
                .MapResult(
                    (ParseZoneModelOptions opts) => (ParseType.ZoneModel, opts.Directory, opts.ZoneGroup, opts.Region, opts.Environment),
                    (BasicOptions opts) => (ParseType.Basic, opts.File, "", "", ""),
                    (SampleOptions opts) => (ParseType.Sample, opts.File, "", "", ""),
                    (InitOptions opts) => (ParseType.Init, opts.RootDirectory, opts.ZoneGroup, opts.Region, opts.Environment),
                    errs => (ParseType.Error, "", "", "", ""));

            return configuredOpts;
        }
    }
}
