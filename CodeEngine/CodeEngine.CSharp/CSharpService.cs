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
        private Script<T> script;

        public void Compile(string code)
        {
            var scriptOptions = ScriptOptions.Default
                .WithImports("System", "Newtonsoft.Json")
                .WithReferences(typeof(JsonConvert).Assembly, typeof(Globals).Assembly);

            script = CSharpScript.Create<T>(code, scriptOptions, typeof(Globals));
            script.Compile();
        }

        public async Task<T> ExecuteAsync(string globalState)
        {
            var imports = new List<string>() { "System", "Newtonsoft.Json" };
            return await ExecuteAsync(globalState, imports);
        }

        public async Task<T> ExecuteAsync(string globalState, IEnumerable<string> imports)
        {
            var types = new List<Assembly>() { typeof(JsonConvert).Assembly, typeof(Globals).Assembly };
            return await ExecuteAsync(globalState, imports, types);
        }

        public async Task<T> ExecuteAsync(string globalState, IEnumerable<string> imports, IEnumerable<Assembly> types)
        {
            var result = await script.RunAsync(new Globals() { GlobalState = globalState });
            if (result.Exception != null)
            {
                throw result.Exception;
            }

            return result.ReturnValue;
        }
    }
}
