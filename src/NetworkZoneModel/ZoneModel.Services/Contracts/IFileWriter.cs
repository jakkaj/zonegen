using System.Threading.Tasks;

namespace ZoneModel.Services.Contracts
{
    public interface IFileWriter
    {
        Task WriteAsYaml<T>(T data, string path, bool replace = false) where T : class;
    }
}
