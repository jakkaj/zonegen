using System;
using System.Collections.Generic;

namespace NetworkZoneModelCli
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            var zoneModelName = "contosoweb.australiaeast.dev";
            var root = "templates/" +zoneModelName.Replace('.','/') ;
            var rulePath = root + "/rule";
            var zonePath = root + "/zone";

            var rules = FileParser.ParseAllInDir<NetworkRule>(rulePath);
            var zones = FileParser.ParseAllInDir<Zone>(zonePath);

            Console.WriteLine($"Got {rules.Count} rules");
            Console.WriteLine($"Got {zones.Count} zones");

            var zoneModel = new ZoneModel() {
                Id = zoneModelName,
                Zones = zones,
                Rules = new List<Rule>(rules)
            };

            Console.WriteLine("Produced a Network Zone Model");

            Console.ReadKey();


        }
    }
}
