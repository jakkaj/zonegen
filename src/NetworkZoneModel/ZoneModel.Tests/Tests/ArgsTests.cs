using System;
using System.Collections.Generic;
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

            Assert.IsTrue(configuredOpts.Item1 == ParseType.Basic);
            Assert.IsTrue(configuredOpts.Item2 == "Some file");

        }

        
    }
}
