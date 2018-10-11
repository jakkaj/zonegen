using System;
using System.Collections.Generic;
using System.Text;
using ZoneModel.Services.Contracts;

namespace ZoneModel.Model
{
    // this class contains samples used to create new yaml files
    public static class Samples
    {
        public static NetworkRule NetworkRuleSample(string id) => new NetworkRule() {
            From = "from-this-zone",
            To = "to-this-zone",
            Id = id,
            IsBidirectional = true,
            Description = "Describe the rule",
            Ports = new List<int> { 22, 80 }
        };

        public static Zone ZoneSample(string id, int index) => new Zone()
        {
            Id = id,
            Index = index
        };

        public static RootModel RootModelSample(ISubnetCalculator calculator)
        {
            var model = new RootModel
            {
                ZoneGroup = "Zone Group Id",
                Regions = new List<Region>()
            };

            var region = new Region { Id = "australiaeast", Environments = new List<Environment>() };
            var environment = new Environment()
            {
                Id = "platform-cnry",
                Cidr = "10.2.0.0/16",
                Rules = new List<Rule> { NetworkRuleSample("rule-id"), new NetworkRule{
                    Id = "rule-id-24691",
                    From = "frontend",
                    To= "backend",
                    Ports = new List<int>{22, 80},
                    IsBidirectional = true,
                    Description = "Frontend should access backend"
                }},
                Zones = new List<Zone> { ZoneSample("backend", 1), ZoneSample("frontend", 2) }
            };
            var hub = new Environment()
            {
                Id = "hub",
                Cidr = "10.2.0.0/16",
                Ignore = true
            };

            environment.Zones.ForEach(z => 
                z.Cidr = calculator.GetSubnetOffset(environment.Cidr, (byte)environment.ZoneSubnetMaskSize, z.Index));

            region.Environments.Add(environment);
            region.Environments.Add(hub);
            model.Regions.Add(region);

            return model;
        }
    }
}
