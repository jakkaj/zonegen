using System.Collections.Generic;
using System.Text;
using ZoneModel.Model;
using ZoneModel.Services.Contracts;

namespace ZoneModel.Services
{
    // this class contains samples used to create new yaml files
    public static class Samples
    {
        public static NetworkRule NetworkRuleSample(string id,
                string from = "from-this-zone",
                string to = "to-this-zone",
                bool isBidirectional = true,
                string description = "Describe the rule" ) => new NetworkRule() {
            From = from,
            To = to,
            Id = id,
            IsBidirectional = isBidirectional,
            Description = description,
            Ports = new List<int> { 22, 80 }
        };

        public static Zone ZoneSample(string id, int index) => new Zone()
        {
            Id = id,
            Index = index
        };

        public static RootModel ConfigSample(string regionId, string envId, string hubId)
        {
            var model = new RootModel();
            model.Regions = new List<Region>
            {
                new Region()
                {
                    Id = regionId,
                    Environments = new List<Environment>
                    {
                        new Environment()
                        {
                            Id = envId,
                            Cidr = "10.2.0.0/16"
                        },
                        new Environment()
                        {
                            Id = hubId,
                            Cidr = "10.1.0.0/16",
                            Ignore = true
                        }
                    }
                }
            };
            return model;
        }

        public static RootModel RootModelSample(ISubnetCalculator calculator)
        {
            var model = new RootModel
            {
                ZoneGroup = "Zone Group Id",
                Regions = new List<Region>()
            };

            var region = new Region { Id = "australiaeast", Environments = new List<Model.Environment>() };
            var environment = new Model.Environment()
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
