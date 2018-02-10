using CodeEngine.CSharp.Interfaces;
using CodeEngine.FSharp.Interfaces;
using CodeEngine.Interfaces;
using CodeEngine.JavaScript.Interfaces;
using CodeEngine.Python.Interfaces;
using System;
using System.Threading.Tasks;

namespace CodeEngine.Services
{
    public class CodeService<TInput, TOutput>
        : ICodeService<TInput, TOutput>
    {
        readonly IFileService fileService;
        readonly ICSharpService<TOutput> cSharpService;
        readonly IFSharpService<TOutput> fSharpService;
        readonly IPythonService<TOutput> pythonService;
        readonly IJavaScriptService<TOutput> javaScriptService;

        public CodeService(IFileService fileService, ICSharpService<TOutput> cSharpService, IFSharpService<TOutput> fSharpService, IPythonService<TOutput> pythonService, IJavaScriptService<TOutput> javaScriptService)
        {
            this.fileService = fileService;
            this.cSharpService = cSharpService; 
            this.fSharpService = fSharpService;
            this.pythonService = pythonService;
            this.javaScriptService = javaScriptService;
        }

        public async Task<TOutput> CompileAsync(TInput scriptLocation, TInput definitionLocation)
        {
            var scriptFileResult = await fileService.FetchFileContentsAsync(scriptLocation);
            var definitionFileResult = await fileService.FetchFileContentsAsync(definitionLocation);
            if (scriptFileResult.FileExtension == ".cscript")
            {
                return await cSharpService.ExecuteAsync(scriptFileResult.FileContents, definitionFileResult.FileContents);
            }
            else if (scriptFileResult.FileExtension == ".fscript")
            {
                return await fSharpService.ExecuteAsync(scriptFileResult.FileContents);
            }
            else if (scriptFileResult.FileExtension == ".pyscript")
            {
                return await pythonService.ExecuteAsync(scriptFileResult.FileContents);
            }
            else if (scriptFileResult.FileExtension == ".jscript")
            {
                return await javaScriptService.ExecuteAsync(scriptFileResult.FileContents);
            }

            throw new InvalidOperationException($"FileExtension from input location {scriptLocation.ToString()} not handled.");
        }
    }
}
