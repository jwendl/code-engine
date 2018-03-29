using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace CodeEngine.CSharp.Interfaces
{
    public interface ICSharpService<T>
    {
        Task<T> ExecuteAsync(string code, string deviceState);

        Task<T> ExecuteAsync(string code, string initialState, IEnumerable<string> imports);

        Task<T> ExecuteAsync(string code, string initialState, IEnumerable<string> imports, IEnumerable<Assembly> types);
    }
}
