using CodeEngine.CSharp.Interfaces;
using CodeEngine.CSharp.Models;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace CodeEngine.CSharp
{
    public class CSharpService<T>
        : ICSharpService<T>
    {
        public async Task<T> ExecuteAsync(string code, string globalState)
        {
            var imports = new List<string>() { "System", "Newtonsoft.Json", "CodeEngine.CSharp" };
            return await ExecuteAsync(code, globalState, imports);
        }

        public async Task<T> ExecuteAsync(string code, string globalState, IEnumerable<string> imports)
        {
            var types = new List<Assembly>() { typeof(JsonConvert).Assembly, typeof(Globals).Assembly };
            return await ExecuteAsync(code, globalState, imports, types);
        }

        public async Task<T> ExecuteAsync(string code, string globalState, IEnumerable<string> imports, IEnumerable<Assembly> types)
        {
            var scriptOptions = ScriptOptions.Default
                .WithImports("System", "Newtonsoft.Json", "CodeEngine.CSharp")
                .WithReferences(typeof(JsonConvert).Assembly, typeof(Globals).Assembly);

            var scriptState = await CSharpScript.RunAsync<T>(code, scriptOptions, globals: new Globals() { GlobalState = globalState });
            if (scriptState.Exception != null)
            {
                throw scriptState.Exception;
            }

            return scriptState.ReturnValue;
        }
    }
}
