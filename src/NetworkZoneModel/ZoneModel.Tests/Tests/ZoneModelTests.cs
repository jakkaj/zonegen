using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZoneModel.Services.Contracts;
using ZoneModel.Tests;

namespace ZoneModel.Tests.Tests
{
    [TestClass]
    public class ZoneModelTests :TestBase
    {
        [TestMethod]
        public async Task TestSimpleParse()
        {
            var parser = Resolve<IZoneModelParser>();

            var model = await parser.Parse("contosoweb", "australiaeast", "dev", "templates");

            Assert.IsNotNull(model);

            Assert.IsTrue(model.ZoneGroup == "contosoweb");
        }
    }
}
