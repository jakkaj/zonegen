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
        public void TestFileArgs()
        {
            var args = new List<string>();
            args.Add("--file");
            args.Add("Some file");

            var msr = new ManualResetEvent(false);

            var signalled = false;

            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed<CommandLineOptions>(o =>
                {
                    Assert.IsTrue(o.File == "Some file");
                    if (!string.IsNullOrWhiteSpace(o.File))
                    {
                        signalled = true;
                        msr.Set();
                        Console.WriteLine($"File sent: -f {o.File}");
                        
                    }
                    else
                    {
                        
                    }
                });

            msr.WaitOne(TimeSpan.FromSeconds(2));

            Assert.IsTrue(signalled);
        }

        [TestMethod]
        public void TestBasicFileArgs()
        {
            var args = new List<string>();
            args.Add("-f");
            args.Add("Some file");

            var msr = new ManualResetEvent(false);

            var signalled = false;

            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed<CommandLineOptions>(o =>
                {
                    Assert.IsTrue(o.File == "Some file");
                    if (!string.IsNullOrWhiteSpace(o.File))
                    {
                        signalled = true;
                        msr.Set();
                        Console.WriteLine($"File sent: -f {o.File}");

                    }
                    else
                    {

                    }
                });

            msr.WaitOne(TimeSpan.FromSeconds(2));

            Assert.IsTrue(signalled);
        }


        [TestMethod]
        public void TestNoVerboseArgs()
        {
            var args = new List<string>();
            

            var msr = new ManualResetEvent(false);

            var signalled = false;

            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed<CommandLineOptions>(o =>
                {
                    signalled = true;
                    msr.Set();
                    Console.WriteLine($"File empty");
                });

            msr.WaitOne(TimeSpan.FromSeconds(2));

            Assert.IsFalse(signalled);
        }
    }
}
