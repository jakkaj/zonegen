
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ZoneModel.Model
{

    // POCO definition of a zone 
    // this is a vertex in the network definition graph
    public class Zone
    {
        [JsonProperty("id")]
        public string Id { get; set; } // internet-facing
        [JsonProperty("parent")]
        public Zone Parent { get; set; }
        [JsonProperty("chidren")]
        public IList<Zone> Children { get; set; } // zones are hierarchial, and a type of edge


    }
}