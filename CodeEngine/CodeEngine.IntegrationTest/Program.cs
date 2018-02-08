using CodeEngine.CSharp;
using CodeEngine.CSharp.Interfaces;
using CodeEngine.FSharp;
using CodeEngine.FSharp.Interfaces;
using CodeEngine.Interfaces;
using CodeEngine.JavaScript;
using CodeEngine.JavaScript.Interfaces;
using CodeEngine.Python;
using CodeEngine.Python.Interfaces;
using CodeEngine.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CodeEngine.IntegrationTest
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IFileService, FileService>();
            serviceCollection.AddScoped<ICSharpService<int>, CSharpService<int>>();
            serviceCollection.AddScoped<IFSharpService<int>, FSharpService<int>>();
            serviceCollection.AddScoped<IPythonService<int>, PythonService<int>>();
            serviceCollection.AddScoped<IJavaScriptService<int>, JavaScriptService<int>>();
            serviceCollection.AddScoped<ICodeService<FileInfo, int>, CodeService<FileInfo, int>>();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            await CSharp(serviceProvider);
            //await FSharp(serviceProvider);
            //await Python(serviceProvider);
            await JavaScript(serviceProvider);

            Console.ReadKey();
        }

        private static async Task CSharp(ServiceProvider serviceProvider)
        {
            var codeService = serviceProvider.GetRequiredService<ICodeService<FileInfo, int>>();
            var result = await codeService.CompileAsync(new FileInfo("test.cscript"));
            Console.WriteLine($"CSharp return value is {result}");
        }

        private static async Task FSharp(ServiceProvider serviceProvider)
        {
            var codeService = serviceProvider.GetRequiredService<ICodeService<FileInfo, int>>();
            var result = await codeService.CompileAsync(new FileInfo("test.fscript"));
            Console.WriteLine($"FSharp return value is {result}");
        }

        private static async Task Python(ServiceProvider serviceProvider)
        {
            var codeService = serviceProvider.GetRequiredService<ICodeService<FileInfo, int>>();
            var result = await codeService.CompileAsync(new FileInfo("test.pyscript"));
            Console.WriteLine($"Python return value is {result}");
        }

        private static async Task JavaScript(ServiceProvider serviceProvider)
        {
            var codeService = serviceProvider.GetRequiredService<ICodeService<FileInfo, int>>();
            var result = await codeService.CompileAsync(new FileInfo("test.jscript"));
            Console.WriteLine($"JavaScript return value is {result}");
        }
    }
}
