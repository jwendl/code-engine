using System.Threading.Tasks;

namespace CodeEngine.FSharp.Interfaces
{
    public interface IFSharpService<T>
    {
        Task<T> ExecuteAsync(string code, string globalState);
    }
}
