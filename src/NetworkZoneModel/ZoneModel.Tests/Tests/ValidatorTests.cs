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
        [ExpectedException(typeof(ZoneModelDefinitionException))]
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
            model.Regions = new List<Region>();
            model.Regions.Add(new Region() {
                Id = "regionId",
                Environments = new List<Environment>
                {
                    new Environment() {
                        Zones = new List<Zone>{sampleZone},
                        Rules = new List<Rule>{sampleRule},
                        Id = "someId"
                    }

                }
            });

            model.ZoneGroup = "AZoneGroup";
            model.Validate();
        }
    }
}
