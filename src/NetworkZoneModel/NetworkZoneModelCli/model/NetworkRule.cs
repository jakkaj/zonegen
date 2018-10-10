using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace NetworkZoneModelCli
{
    //this is an edge in the zone model graph
    public class NetworkRule : Rule 
    {
        [JsonProperty("ports")]
        public IList<int> Ports {get;set;}
    }
}