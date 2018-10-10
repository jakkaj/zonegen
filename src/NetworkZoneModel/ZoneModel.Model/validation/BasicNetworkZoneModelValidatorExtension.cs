﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZoneModel.Model.Validation
{
    public static class BasicNetworkZoneModelValidatorExtension
    {
        public static void Validate(this RootModel model)
        {
            if (string.IsNullOrEmpty(model.ZoneGroup))
            {
                throw new BadZoneDefinitionException("Zone Model ZoneGroup cannot be null or empty");
            }
            if(model.Zones == null || !model.Zones.Any())
            {
                throw new BadZoneDefinitionException("Zone List cannot be empty");
            }
        }
    }
}