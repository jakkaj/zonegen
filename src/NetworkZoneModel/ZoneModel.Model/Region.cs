using System;
using System.Collections.Generic;
using System.Text;

namespace ZoneModel.Model
{
    public class Region
    {
        public string Id { get; set; }
        public List<Environment> Environments { get; set; }
    }
}
