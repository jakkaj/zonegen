using ZoneModel.Model;

namespace ZoneModel.Services.Contracts
{
    public interface IGraphWriter
    {
        string WriteGraph(RootModel model, string file = null);
    }
}