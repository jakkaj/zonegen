using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZoneModel.Model;
using ZoneModel.Model.Validation;
using ZoneModel.Services;
using ZoneModel.Services.Contracts;
using ZoneModel.Tests;

namespace ZoneModel.Tests.Tests
{
    [TestClass]
    public class FileWriterTests :TestBase
    {
        [TestMethod]
        public async Task TestSimpleParse()
        {
            var writer = Resolve<IFileWriter>();
            var calculator = Resolve<ISubnetCalculator>();

            var model = Samples.RootModelSample(calculator);
            //var model = await parser.Parse("contosoweb", "australiaeast", "dev", "templates");

            Assert.IsNotNull(model);
            model.Validate();

            var dir = Directory.GetCurrentDirectory();
            var path = Path.Join(dir, "variables.yaml");
            await writer.WriteAsYaml(model, path, true);

            Assert.IsTrue(File.Exists(path));
        }
    }
}
