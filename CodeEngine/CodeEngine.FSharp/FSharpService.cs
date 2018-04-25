using CodeEngine.FSharp.Interfaces;
using Microsoft.FSharp.Core;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.FSharp.Compiler.Interactive.Shell;
using static Microsoft.FSharp.Compiler.ReferenceResolver;

namespace CodeEngine.FSharp
{
    public class FSharpService<T>
        : IFSharpService<T>
    {
        public async Task<T> ExecuteAsync(string code, string globalState)
        {
            var arguments = new List<string>()
            {
                "fsi.exe",
                "--noninteractive",
                "--nologo",
                "--gui-"
            }.ToArray();

            var stringReader = new StringReader(globalState);
            var outputStringBuilder = new StringBuilder();
            var errorStringBuilder = new StringBuilder();
            var outputStream = new StringWriter(outputStringBuilder);
            var errorStream = new StringWriter(errorStringBuilder);
            var fSharpOptions = new FSharpOption<bool>(true);
            var resolverOptions = new FSharpOption<Resolver>(default(Resolver));
            var sessionConfiguration = FsiEvaluationSession.GetDefaultConfiguration();
            var evaluationSession = FsiEvaluationSession.Create(sessionConfiguration, arguments, stringReader, outputStream, errorStream, fSharpOptions, resolverOptions);

            var result = evaluationSession.EvalExpression(code);
            var fSharpValue = await Task.FromResult(result.Value);
            return (T)fSharpValue.ReflectionValue;
        }
    }
}
