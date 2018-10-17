using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GraphVizWrapper;
using ZoneModel.Model;
using ZoneModel.Services.Contracts;

namespace ZoneModel.Services.Utils
{
    public class GraphWriter : IGraphWriter
    {
        public string WriteGraph(RootModel model, string file = null)
        {

            var regionNodes = new Node("record", "filled", "yellow");
            var envNodes = new Node("doublecircle", "filled", "lightblue");
            var zoneNodes = new Node("doublecircle", "filled", "lightgreen");

            var edgesTo = new Edge("bold", null, null);
            //var edgesFrom = new Edge("bold", null, null);
            var edgesBoth = new Edge("bold", null, "both");
            foreach (var region in model.Regions)
            {
                regionNodes.Elements.Add(region.Id);

                foreach (var env in region.Environments)
                {
                    envNodes.Elements.Add(env.Id);
                    edgesTo.Connections.Add(new Connection(region.Id, env.Id, "label"));

                    foreach (var zone in env.Zones)
                    {
                        edgesTo.Connections.Add(new Connection(env.Id, zone.Id, "label"));
                        zoneNodes.Elements.Add(zone.Id);
                    }

                    foreach (var rule in env.Rules)
                    {
                        if (rule.IsBidirectional)
                        {
                            edgesBoth.Connections.Add(new Connection(rule.From, rule.To, rule.Description));
                            //need a bidirectional edge
                        }
                        else
                        {
                            edgesTo.Connections.Add(new Connection(rule.From, rule.To, rule.Description));
                        }
                    }
                }

            }

            var graphBuilder = new GraphBuilder();

            var nodes = new List<Node>
            {
                regionNodes, envNodes, zoneNodes
            };

            var edges = new List<Edge>
            {
                edgesTo, edgesBoth
            };

            var dotGraph = graphBuilder.Build(nodes, edges, model.ZoneGroup);
            if (!string.IsNullOrWhiteSpace(file))
            {
                File.WriteAllText(file, dotGraph);
            }


            return dotGraph;
        } 
    }
}
