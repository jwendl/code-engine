using CodeEngine.Models;
using System.Threading.Tasks;

namespace CodeEngine.Interfaces
{
    public interface IFileService
    {
        Task<FileServiceResult> FetchFileContentsAsync<TInput>(TInput location);
    }
}
