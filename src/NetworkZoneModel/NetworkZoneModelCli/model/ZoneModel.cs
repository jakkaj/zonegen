

using System.Collections.Generic;

namespace NetworkZoneModelCli
{
    public class ZoneModel
    {
        public string Id { get; set; }
        public IList<Zone> Zones { get; set; }
        public IList<Rule> Rules { get; set; }

    }
}
