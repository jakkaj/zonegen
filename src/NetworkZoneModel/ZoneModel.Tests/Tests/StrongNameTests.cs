using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using ZoneModel.Model;
using ZoneModel.Model.Validation;
using ZoneModel.Services;
using ZoneModel.Services.Contracts;

namespace ZoneModel.Tests.Tests
{
    [TestClass]
    public class StrongNameTests : TestBase
    {
        [TestMethod]
        public void ValidateStrongNames()
        {
            var calculator = Resolve<ISubnetCalculator>();
            var model = Samples.RootModelSample(calculator);
            model.Validate();

            model.ToStrongIds();

            // collect all the ids and assert none are the same
            // this will throw with dup keys if there are multi ids
            var ids = new Dictionary<string, object>();
            ids.Add(model.ZoneGroup, model); // tread zonegroup as an id
            foreach(var reg in model.Regions)
            {
                ids.Add(reg.Id, reg);
                foreach(var env in reg.Environments)
                {
                    ids.Add(env.Id, env);
                    foreach(var zone in env.Zones)
                    {
                        ids.Add(zone.Id, zone);
                    }
                }
            }
        }

    }
}
