using CodeEngine.CSharp.Interfaces;
using CodeEngine.CSharp.Models;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CodeEngine.CSharp
{
    public class CSharpService<T>
        : ICSharpService<T>
    {
        public async Task<T> ExecuteAsync(string code, string deviceTypeDefinition)
        {
            var scriptOptions = ScriptOptions.Default
                .WithImports("System", "Newtonsoft.Json", "CodeEngine.CSharp")
                .WithReferences(typeof(JsonConvert).Assembly, typeof(Globals).Assembly);

            var deviceState = @"{ }";
            var scriptState = await CSharpScript.RunAsync<T>(code, scriptOptions, globals: new Globals() { DeviceTypeDefinition = deviceTypeDefinition, DeviceState = deviceState });
            if (scriptState.Exception != null)
            {
                throw scriptState.Exception;
            }

            return scriptState.ReturnValue;
        }
    }
}
