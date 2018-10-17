//
// Port from https://github.com/jakkaj/k-scratch-node/blob/master/src/model/configGrapher/dotgraph.ts
//
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphVizWrapper
{
    public class Connection
    {
        private readonly string _from;
        private readonly string _to;
        private readonly string _label;

        public Connection(string from, string to, string label)
        {
            _from = @from;
            _to = to;
            _label = label;
        }

        public string Build()
        {
            var builder = $"\"{this._from}\"-> \"{this._to}\"";

            if (!string.IsNullOrWhiteSpace(this._label))
            {
                builder +=  $"[ label = \"      {this._label}     \" ]";
            }     

            return builder;   
        }
    }
}
