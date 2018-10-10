using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using YamlDotNet.Serialization;

namespace ZoneModel.Model
{
    //this is an edge in the zone model graph
    public class NetworkRule : Rule 
    {
        [JsonProperty("ports")]
        [YamlMember(Order = 6)]
        public List<int> Ports {get;set;}
    }
}