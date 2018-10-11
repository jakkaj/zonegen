using ZoneModel.Services.Options;

namespace ZoneModel.Services.Contracts
{
    public interface IOptionsParser
    {
        OptionResultBase ParseArgs(string[] args);
    }
}