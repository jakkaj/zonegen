﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkZoneModelCli.Validation
{
    public static class BasicNetworkZoneModelValidatorExtension
    {
        public static void Validate(this ZoneModel model)
        {
            if (string.IsNullOrEmpty(model.Id))
            {
                throw new BadZoneDefinitionException("Zone Model Id cannot be null or empty");
            }
            if(model.Zones == null || !model.Zones.Any())
            {
                throw new BadZoneDefinitionException("Zone List cannot be empty");
            }
        }
    }
}
