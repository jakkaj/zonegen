using System;
using System.Collections.Generic;
using System.Text;

namespace ZoneModel.Model
{
    public class Environment
    {
        public string Id { get; set; }
        public bool Ignore { get; set; }
        public string Cidr { get; set; }
        public int ZoneSubnetMaskSize { get; set; } = 24;
        public List<Zone> Zones { get; set; } = new List<Zone>();
        public List<Rule> Rules { get; set; } = new List<Rule>(); 
    }
}
