using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace CodeEngine.CSharp.Interfaces
{
    public interface ICSharpService<T>
    {
        void Compile(string code);

        Task<T> ExecuteAsync(string deviceState);

        Task<T> ExecuteAsync(string initialState, IEnumerable<string> imports);

        Task<T> ExecuteAsync(string initialState, IEnumerable<string> imports, IEnumerable<Assembly> types);
    }
}
