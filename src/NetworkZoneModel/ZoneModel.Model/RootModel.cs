

using System;
using System.Collections.Generic;

namespace ZoneModel.Model
{
    public class RootModel
    {
        // ZoneGroup > Region > Environment > Zone
        public string ZoneGroup { get; set; }
        public List<Region> Regions { get; set; }

    }
}
