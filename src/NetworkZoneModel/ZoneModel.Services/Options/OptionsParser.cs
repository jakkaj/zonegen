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
        public OptionResultBase ParseArgs(string[] args)
        {
            var configuredOpts = CommandLine.Parser.Default.ParseArguments<
                    ParseZoneModelOptions, 
                    BasicOptions, 
                    SampleOptions,
                    InitOptions>(args)
                .MapResult<ParseZoneModelOptions, BasicOptions, SampleOptions, InitOptions, OptionResultBase>(
                    (ParseZoneModelOptions opts) => opts,
                    (BasicOptions opts) => opts,
                    (SampleOptions opts) => opts,
                    (InitOptions opts) => opts,
                    errs => new ErrorOption()
                    );

            return configuredOpts;
        }
    }
}
