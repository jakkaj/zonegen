using System;
using System.Collections.Generic;
using System.Text;

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
            Ports = new List<int> { 22, 80 }
        };

        public static Zone ZoneExample(string id) => new Zone()
        {
            Id = id
        };
    }
}
