using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace ZoneModel.Model
{
    //this is an edge in the zone model graph
    public abstract class Rule 
    {
        [JsonProperty("id")]
        [YamlMember(Order = 1)]
        public string Id {get;set;}
        [JsonProperty("from")]
        [YamlMember(Order = 2)]
        public string From {get;set;} // this is a zone id
        [JsonProperty("to")]
        [YamlMember(Order = 3)]
        public string To {get;set;} // this is a zone id
        [JsonProperty("isBidirectional")]
        [YamlMember(Order = 4)]
        public bool IsBidirectional {get;set;} // set true for a second rule in reverse direction
    }
}