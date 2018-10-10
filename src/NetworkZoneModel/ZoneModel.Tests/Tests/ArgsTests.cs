using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CommandLine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZoneModel.Services.Options;

namespace ZoneModel.Tests.Tests
{
    [TestClass]
    public class ArgsTests : TestBase
    {
        [TestMethod]
        public void TestVerboseArgs()
        {
            var args = new List<string>();
            args.Add("--verbose");

            var msr = new ManualResetEvent(false);

            var signalled = false;

            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed<CommandLineOptions>(o =>
                {
                    if (o.Verbose)
                    {
                        signalled = true;
                        msr.Set();
                        Console.WriteLine($"Verbose output enabled. Current Arguments: -v {o.Verbose}");
                        Console.WriteLine("Quick Start Example! App is in Verbose mode!");
                    }
                    else
                    {
                        Console.WriteLine($"Current Arguments: -v {o.Verbose}");
                        Console.WriteLine("Quick Start Example!");
                    }
                });

            msr.WaitOne(TimeSpan.FromSeconds(2));

            Assert.IsTrue(signalled);
        }

        [TestMethod]
        public void TestBasicVerboseArgs()
        {
            var args = new List<string>();
            args.Add("-v");

            var msr = new ManualResetEvent(false);

            var signalled = false;

            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed<CommandLineOptions>(o =>
                {
                    if (o.Verbose)
                    {
                        signalled = true;
                        msr.Set();
                        Console.WriteLine($"Verbose output enabled. Current Arguments: -v {o.Verbose}");
                        Console.WriteLine("Quick Start Example! App is in Verbose mode!");
                    }
                    else
                    {
                        Console.WriteLine($"Current Arguments: -v {o.Verbose}");
                        Console.WriteLine("Quick Start Example!");
                    }
                });

            msr.WaitOne(TimeSpan.FromSeconds(2));

            Assert.IsTrue(signalled);
        }


        [TestMethod]
        public void TestNoVerboseArgs()
        {
            var args = new List<string>();
            args.Add("");

            var msr = new ManualResetEvent(false);

            var signalled = false;

            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed<CommandLineOptions>(o =>
                {
                    if (o.Verbose)
                    {
                        
                        Console.WriteLine($"Verbose output enabled. Current Arguments: -v {o.Verbose}");
                        Console.WriteLine("Quick Start Example! App is in Verbose mode!");
                    }
                    else
                    {
                        signalled = true;
                        msr.Set();
                        Console.WriteLine($"Current Arguments: -v {o.Verbose}");
                        Console.WriteLine("Quick Start Example!");
                    }
                });

            msr.WaitOne(TimeSpan.FromSeconds(2));

            Assert.IsTrue(signalled);
        }
    }
}
