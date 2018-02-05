using CodeEngine.Models;
using System.Threading.Tasks;

namespace CodeEngine.Interfaces
{
    public interface ICodeService<TInput, TOutput>
    {
        Task<CodeServiceResult<TOutput>> CompileAsync(TInput location);
    }
}
