using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkZoneModelCli.Validation
{
    public class BadZoneDefinitionException : System.Exception
    {
        public BadZoneDefinitionException(string message): base(message)
        {
        }
    }
}
