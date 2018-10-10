using System.Collections.Generic;

namespace NetworkZoneModelCli
{
    //this is an edge in the zone model graph
    public class RuleSet
    {
        public string Id {get;set;}
        public IList<Rule> Rules {get;set;}
    }
}