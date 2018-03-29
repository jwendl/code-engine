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
        public async Task<T> ExecuteAsync(string code, string initialState)
        {
            var imports = new List<string>() { "System", "Newtonsoft.Json", "CodeEngine.CSharp" };
            return await ExecuteAsync(code, initialState, imports);
        }

        public async Task<T> ExecuteAsync(string code, string initialState, IEnumerable<string> imports)
        {
            var types = new List<Assembly>() { typeof(JsonConvert).Assembly, typeof(Globals).Assembly };
            return await ExecuteAsync(code, initialState, imports, types);
        }

        public async Task<T> ExecuteAsync(string code, string initialState, IEnumerable<string> imports, IEnumerable<Assembly> types)
        {
            var scriptOptions = ScriptOptions.Default
                .WithImports("System", "Newtonsoft.Json", "CodeEngine.CSharp")
                .WithReferences(typeof(JsonConvert).Assembly, typeof(Globals).Assembly);

            var scriptState = await CSharpScript.RunAsync<T>(code, scriptOptions, globals: new Globals() { InitialState = initialState });
            if (scriptState.Exception != null)
            {
                throw scriptState.Exception;
            }

            return scriptState.ReturnValue;
        }
    }
}
