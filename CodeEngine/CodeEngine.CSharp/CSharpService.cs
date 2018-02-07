using CodeEngine.CSharp.Interfaces;
using CodeEngine.CSharp.Models;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Threading.Tasks;

namespace CodeEngine.CSharp
{
    public class CSharpService<T>
        : ICSharpService<T>
    {
        public async Task<CSharpCodeResult<T>> ExecuteAsync(string code)
        {
            var scriptState = await CSharpScript.RunAsync<T>(code);
            return new CSharpCodeResult<T>()
            {
                Exception = scriptState.Exception,
                ReturnValue = scriptState.ReturnValue,
            };
        }
    }
}
