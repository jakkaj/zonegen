
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
        
        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("cidr")]
        public string Cidr { get; set; }
    }
}