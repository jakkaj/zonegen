//
// Port from https://github.com/jakkaj/k-scratch-node/blob/master/src/model/configGrapher/dotgraph.ts
//

using System;
using System.Collections.Generic;
using System.Text;

namespace GraphVizWrapper
{
    public class GraphBuilder
    {
        public string Build(List<Node> nodes, List<Edge> edges, string title)
        {
            var builder = "digraph app {\r\n";
            builder += "graph [ fontname=Consolas,center=true," +
                       "nodesep=.8," +
                       "ranksep=\".2 equally\"," +
                       "sep=2.2" +
                       "];";
            foreach (var node in nodes)
            {
                builder += "\r\n" + node.Build();
            }

            foreach (var edge in edges)
            {
                builder += "\r\n" + edge.Build();
            }

            builder += "labelloc=\"t\";\r\nlabel=\"" + title + "\";";

            builder += "\r\n}";

            return builder;
        }
    }
}
