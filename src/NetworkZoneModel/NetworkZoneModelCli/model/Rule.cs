using Newtonsoft.Json;

namespace NetworkZoneModelCli
{
    //this is an edge in the zone model graph
    public abstract class Rule 
    {
        [JsonProperty("id")]
        public string Id {get;set;}
        [JsonProperty("from")]
        public string From {get;set;} // this is a zone id
        [JsonProperty("to")]
        public string To {get;set;} // this is a zone id
        [JsonProperty("isBidirectional")]
        public bool IsBidirectional {get;set;} // set true for a second rule in reverse direction
    }
}