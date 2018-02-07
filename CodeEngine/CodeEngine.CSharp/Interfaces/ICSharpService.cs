using CodeEngine.CSharp.Models;
using System.Threading.Tasks;

namespace CodeEngine.CSharp.Interfaces
{
    public interface ICSharpService<T>
    {
        Task<CSharpCodeResult<T>> ExecuteAsync(string code);
    }
}
