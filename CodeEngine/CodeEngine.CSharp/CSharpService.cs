using CodeEngine.CSharp.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Threading.Tasks;

namespace CodeEngine.CSharp
{
    public class CSharpService<T>
        : ICSharpService<T>
    {
        public async Task<T> ExecuteAsync(string code)
        {
            var scriptState = await CSharpScript.RunAsync<T>(code);
            if (scriptState.Exception != null)
            {
                throw scriptState.Exception;
            }

            return scriptState.ReturnValue;
        }
    }
}
