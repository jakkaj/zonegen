//
// Port from https://github.com/jakkaj/k-scratch-node/blob/master/src/model/configGrapher/dotgraph.ts
//
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphVizWrapper
{
    public class Node
    {
        private readonly string _shape;
        private readonly string _style;
        private readonly string _color;
        public List<string> Elements { get; }

        public Node(string shape, string style, string color, List<string> elements = null)
        {
            _shape = shape;
            _style = style;
            _color = color;
            Elements = elements ?? new List<string>();
        }

        public string Build()
        {
            var builder =
                $"node[margin = .3, fontname = Consolas, fontsize = 10, shape ={this._shape},style ={this._style},color ={this._color}]";

            foreach (var ele in this.Elements)
            {
                builder += "\r\n \"" + ele + "\"";
            }

            return builder;
        }
    }
}
