using System;
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
                throw new ZoneModelDefinitionException("Zone Model ZoneGroup cannot be null or empty");
            }
            if(model.Regions == null || !model.Regions.Any())
            {
                throw new ZoneModelDefinitionException("Region List cannot be empty");
            }
            if(model.Regions.Any( r => r == null || 
                r.Environments == null || 
                string.IsNullOrEmpty(r.Id) || 
                r.Environments.Count == 0))
            {
                throw new ZoneModelDefinitionException($"Environment has badly configured Region");
            }
        }
    }
}
