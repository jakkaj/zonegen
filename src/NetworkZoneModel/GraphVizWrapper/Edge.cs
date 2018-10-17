//
// Port from https://github.com/jakkaj/k-scratch-node/blob/master/src/model/configGrapher/dotgraph.ts
//
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphVizWrapper
{
    public class Edge
    {
        private readonly string _style;
        private readonly string _arrowhead;
        private readonly string _direction;
        public List<Connection> Connections { get; }

        public Edge(string style, string arrowhead, string direction, List<Connection> connections = null)
        {
            _style = style;
            _arrowhead = arrowhead;
            _direction = direction;
            Connections = connections ?? new List<Connection>();
        }

        public string Build()
        {
            var attr = new List<string>();

            if (!string.IsNullOrWhiteSpace(this._style))
            {
                attr.Add("style=" + this._style);
            }

            if (!string.IsNullOrWhiteSpace(_arrowhead))
            {
                attr.Add("arrowhead=" + this._arrowhead);
            }

            if (!string.IsNullOrWhiteSpace(this._direction))
            {
                attr.Add("dir=" + this._direction);
            }

            var style = string.Join(", ", attr);

            var builder = "edge [fontname=Consolas,fontsize=10, ";

            if (!string.IsNullOrWhiteSpace(style)  && style != "")
            {
                builder += " " + style;
            }

            builder += " ]";

            foreach (var ele in this.Connections)
            {
                builder += "\r\n " + ele.Build();
            }

            return builder;
        }
    }
}
