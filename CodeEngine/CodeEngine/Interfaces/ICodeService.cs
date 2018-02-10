using System.Threading.Tasks;

namespace CodeEngine.Interfaces
{
    public interface ICodeService<TInput, TOutput>
    {
        Task<TOutput> CompileAsync(TInput scriptLocation, TInput definitionLocation);
    }
}
