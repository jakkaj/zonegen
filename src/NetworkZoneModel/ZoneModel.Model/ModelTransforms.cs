using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZoneModel.Model
{
    public static class ModelTransforms
    {
        public static void ToStrongIds (this RootModel model)
        {
            foreach(var region in model.Regions)
            {
                foreach(var env in region.Environments)
                {
                    foreach(var zone in env.Zones)
                    {
                        zone.Id = $"{model.ZoneGroup}.{region.Id}.{env.Id}.{zone.Id}";
                    }
                    foreach(var rule in env.Rules)
                    {
                        rule.From = ResolveStrongName(rule.From, env.Zones);
                        rule.To = ResolveStrongName(rule.To, env.Zones);
                    }
                    env.Id = $"{model.ZoneGroup}.{region.Id}.{env.Id}";
                }
                region.Id = $"{model.ZoneGroup}.{region.Id}";
            }
        }

        private static string ResolveStrongName(string id, List<Zone> zones)
        {
            return zones.FirstOrDefault(z => z.Id.Contains(id))?.Id;
        }

        private static bool IsStrongNamed(string id)
        {
            return !string.IsNullOrEmpty(id) && id.Contains(".");
        }
    }
}
