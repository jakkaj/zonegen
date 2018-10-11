using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZoneModel.Services.Contracts;

namespace ZoneModel.Tests.Tests
{
    [TestClass]
    public class SubnetTests : TestBase
    {
        [TestMethod]
        public void GetSubnetOffset()
        {
            var calc = Resolve<ISubnetCalculator>();

            var ip = "10.2.0.0/16";
            byte mask = 24;

            
            Assert.AreEqual(calc.GetSubnetOffset(ip, mask, 1), "10.2.1.0/24");
            Assert.AreEqual(calc.GetSubnetOffset(ip, mask, 2), "10.2.2.0/24");

            Assert.AreEqual(calc.GetSubnetOffset(ip, mask, 22), "10.2.22.0/24");

            Assert.ThrowsException<InvalidOperationException>(() => calc.GetSubnetOffset(ip, mask, 256));
        }
    }
}
