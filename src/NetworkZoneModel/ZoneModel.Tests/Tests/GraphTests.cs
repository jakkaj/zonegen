using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZoneModel.Services.Contracts;

namespace ZoneModel.Tests.Tests
{
    [TestClass]
    public class GraphTests : TestBase
    {
        [TestMethod]
        public void TestSimpleParse()
        {
            var parser = Resolve<IZoneModelParser>();
            var grapher = Resolve<IGraphWriter>();

            var model = parser.Parse("contosoweb", "australiaeast", "dev", "templates");

            Assert.IsNotNull(model);

            Assert.IsTrue(model.ZoneGroup == "contosoweb");

            var g = grapher.WriteGraph(model);



        }
    }
}
