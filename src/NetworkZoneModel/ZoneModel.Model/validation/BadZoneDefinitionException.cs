using System;
using System.Collections.Generic;
using System.Text;

namespace ZoneModel.Model.Validation
{
    public class ZoneModelDefinitionException : System.Exception
    {
        public ZoneModelDefinitionException(string message): base(message)
        {
        }
    }
}
