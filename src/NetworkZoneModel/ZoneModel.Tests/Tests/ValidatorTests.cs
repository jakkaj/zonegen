using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZoneModel.Model;
using ZoneModel.Model.Validation;

namespace ZoneModel.Tests.Tests
{
    [TestClass]
    public class ValidatorTests : TestBase
    {
        [TestMethod]
        [ExpectedException(typeof(BadZoneDefinitionException))]
        public void TestValidatorThrowsOnEmptyInput()
        {
            var model = new RootModel();
            model.Validate();
        }

        [TestMethod]
        public void TestValidatorPassesSamples()
        {
            var model = new RootModel();
            var sampleRule = Samples.NetworkRuleSample("myId");
            var sampleZone = Samples.ZoneExample("zoneId");
            model.Rules = new List<Rule> { sampleRule };
            model.Zones = new List<Zone> { sampleZone };
            model.ZoneGroup = "AZoneGroup";
            model.Validate();
        }
    }
}
