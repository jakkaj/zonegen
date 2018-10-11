using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using CommandLine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZoneModel.Services.Contracts;
using ZoneModel.Services.Options;

namespace ZoneModel.Tests.Tests
{
    [TestClass]
    public class ArgsTests : TestBase
    {
        [TestMethod]
        public void TestFileArgs()
        {
            var args = new List<string>();
            args.Add("basic");
            args.Add("--file");
            args.Add("Some file");

            var opts = Resolve<IOptionsParser>();
            var configuredOpts = opts.ParseArgs(args.ToArray());

            Assert.IsTrue(configuredOpts.ParseType == ParseType.Basic);
            Assert.IsInstanceOfType(configuredOpts, typeof(BasicOptions));
            var basic = configuredOpts as BasicOptions;
            Assert.IsTrue(basic.File == "Some file");

        }
        [TestMethod]
        public void TestInitArgs()
        {
            var region = "australiaeast";
            var group = "groupp";
            var env = "dev";
            var args = new List<string> {
                "init", "-g", group, "-r", region, "-e", env
            };
            var parser = Resolve<IOptionsParser>();
            var result = parser.ParseArgs(args.ToArray());

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(InitOptions));
            var initOptions = result as InitOptions;
            Assert.AreEqual(initOptions.Region, region);
            Assert.AreEqual(initOptions.ZoneGroup, group);
            Assert.AreEqual(initOptions.Environment, env);
        }

        [TestMethod]
        public void TestSampleArgs()
        {
            var outputPath = Path.Join(Directory.GetCurrentDirectory(), "temp.yaml");
            var args = new List<string> {
                "sample", "-o", outputPath
            };
            var parser = Resolve<IOptionsParser>();
            var result = parser.ParseArgs(args.ToArray());

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SampleOptions));
            var initOptions = result as SampleOptions;
            Assert.AreEqual(initOptions.File, outputPath);
        }

        [TestMethod]
        public void TestParseZoneModelArgs()
        {
            // d templates -g testgroup --write
            var dir = "templates";
            var group = "grouppy";
            var args = new List<string> {
                "parse", "-d", dir, "-g", group
            };
            var parser = Resolve<IOptionsParser>();
            var result = parser.ParseArgs(args.ToArray());

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ParseZoneModelOptions));
            var initOptions = result as ParseZoneModelOptions;
            Assert.AreEqual(initOptions.ZoneGroup, group);
            Assert.AreEqual(initOptions.Directory, dir);
        }

    }
}
