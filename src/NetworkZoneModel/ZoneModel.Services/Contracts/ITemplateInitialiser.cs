using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZoneModel.Services.Contracts
{
    public interface ITemplateInitialiser
    {
        string CreateDirectoryStructure(string rootDir, string zoneGroup, string region, string env);
        Task WriteConfigFile();
        Task WriteZonesFile(string zoneId);
        Task WriteRuleFile(string ruleId);
    }
}
