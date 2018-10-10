

using System.Collections.Generic;

namespace NetworkZoneModelCli
{
    public class ZoneModel
    {
        // ZoneGroup > Region > Environment > Zone
        public string ZoneGroup { get; set; }
        public List<Zone> Zones { get; set; }
        public List<Rule> Rules { get; set; }

    }
}
