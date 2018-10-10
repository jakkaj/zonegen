using System.Threading.Tasks;
using ZoneModel.Model;

namespace ZoneModel.Services.Contracts
{
    public interface IZoneModelParser
    {
        Task<RootModel> Parse(string zoneGroup, string region, string environment,
            string basePath = null);
    }
}