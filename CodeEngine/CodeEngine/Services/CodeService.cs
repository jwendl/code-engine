using CodeEngine.CSharp.Interfaces;
using CodeEngine.FSharp.Interfaces;
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
        readonly ICSharpService<TOutput> cSharpService;
        readonly IFSharpService<TOutput> fSharpService;

        public CodeService(IFileService fileService, ICSharpService<TOutput> cSharpService, IFSharpService<TOutput> fSharpService)
        {
            this.fileService = fileService;
            this.cSharpService = cSharpService;
            this.fSharpService = fSharpService;
        }

        public async Task<CodeServiceResult<TOutput>> CompileAsync(TInput location)
        {
            var fileServiceResult = await fileService.FetchFileContentsAsync(location);
            if (fileServiceResult.FileExtension == ".cscript")
            {
                var codeResult = await cSharpService.ExecuteAsync(fileServiceResult.FileContents);
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
            else if (fileServiceResult.FileExtension == ".fscript")
            {
                var codeResult = await fSharpService.ExecuteAsync(fileServiceResult.FileContents);
                //var exceptions = new List<Exception>();
                //if (codeResult.Exception != null)
                //{
                //    exceptions.Add(codeResult.Exception);
                //}

                return new CodeServiceResult<TOutput>()
                {
                    ReturnValue = codeResult,
                };
            }

            throw new InvalidOperationException($"FileExtension from input location {location.ToString()} not handled.");
        }
    }
}
