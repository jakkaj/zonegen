using System;
using System.Collections.Generic;
using System.Text;

namespace ZoneModel.Services.Options
{
    public class ErrorOption : OptionResultBase
    {
        public override ParseType ParseType => ParseType.Error;
    }
}
