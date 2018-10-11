using System.Collections.Generic;

namespace ZoneModel.Model
{
    //this is an edge in the zone model graph
    public class RuleSet
    {
        public string Id {get;set;}
        public List<Rule> Rules {get;set;}
    }
}