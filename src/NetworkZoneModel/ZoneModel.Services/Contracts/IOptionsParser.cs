using ZoneModel.Services.Options;

namespace ZoneModel.Services.Contracts
{
    public interface IOptionsParser
    {
        (ParseType, string,string,string, string, bool) ParseArgs(string[] args);
    }
}