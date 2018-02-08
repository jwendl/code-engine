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

        public async Task<TOutput> CompileAsync(TInput location)
        {
            var fileServiceResult = await fileService.FetchFileContentsAsync(location);
            if (fileServiceResult.FileExtension == ".cscript")
            {
                return await cSharpService.ExecuteAsync(fileServiceResult.FileContents);
            }
            else if (fileServiceResult.FileExtension == ".fscript")
            {
                return await fSharpService.ExecuteAsync(fileServiceResult.FileContents);
            }
            else if (fileServiceResult.FileExtension == ".pyscript")
            {
                return await pythonService.ExecuteAsync(fileServiceResult.FileContents);
            }
            else if (fileServiceResult.FileExtension == ".jscript")
            {
                return await javaScriptService.ExecuteAsync(fileServiceResult.FileContents);
            }

            throw new InvalidOperationException($"FileExtension from input location {location.ToString()} not handled.");
        }
    }
}
