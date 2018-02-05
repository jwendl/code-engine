using CodeEngine.CSharp.Interfaces;
using CodeEngine.Interfaces;
using CodeEngine.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeEngine.Services
{
    public class CodeService<TInput, TOutput>
        : ICodeService<TInput, TOutput>
    {
        readonly IFileService fileService;
        readonly ICSharpService<TOutput> csharpService;

        public CodeService(IFileService fileService, ICSharpService<TOutput> csharpService)
        {
            this.fileService = fileService;
            this.csharpService = csharpService;
        }

        public async Task<CodeServiceResult<TOutput>> CompileAsync(TInput location)
        {
            var fileServiceResult = await fileService.FetchFileContentsAsync(location);
            if (fileServiceResult.FileExtension == ".cscript")
            {
                var codeResult = await csharpService.CompileAsync(fileServiceResult.FileContents);
                var exceptions = new List<Exception>();
                if (codeResult.Exception != null)
                {
                    exceptions.Add(codeResult.Exception);
                }

                return new CodeServiceResult<TOutput>()
                {
                    Exceptions = exceptions,
                    ReturnValue = codeResult.ReturnValue,
                };
            }

            throw new InvalidOperationException($"FileExtension from input location {location.ToString()} not handled.");
        }
    }
}
