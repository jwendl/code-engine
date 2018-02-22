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
            serviceCollection.AddScoped<ICSharpService<string>, CSharpService<string>>();
            serviceCollection.AddScoped<IFSharpService<string>, FSharpService<string>>();
            serviceCollection.AddScoped<IPythonService<string>, PythonService<string>>();
            serviceCollection.AddScoped<IJavaScriptService<string>, JavaScriptService<string>>();
            serviceCollection.AddScoped<ICodeService<FileInfo, string>, CodeService<FileInfo, string>>();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            await Fridge(serviceProvider);
            //await CSharp(serviceProvider);
            //await FSharp(serviceProvider);
            //await Python(serviceProvider);
            //await JavaScript(serviceProvider);

            Console.ReadKey();
        }

        private static async Task Fridge(ServiceProvider serviceProvider)
        {
            var codeService = serviceProvider.GetRequiredService<ICodeService<FileInfo, string>>();
            var result = await codeService.CompileAsync(new FileInfo(Path.Combine("scripts", "fridge.cscript")), new FileInfo(Path.Combine("DeviceInfo", "fridge.json")));
            Console.WriteLine($"CSharp return value is {result}");
        }

        private static async Task CSharp(ServiceProvider serviceProvider)
        {
            var codeService = serviceProvider.GetRequiredService<ICodeService<FileInfo, int>>();
            var result = await codeService.CompileAsync(new FileInfo(Path.Combine("scripts", "test.cscript")), new FileInfo(Path.Combine("DeviceInfo", "test.json")));
            Console.WriteLine($"CSharp return value is {result}");
        }

        private static async Task FSharp(ServiceProvider serviceProvider)
        {
            var codeService = serviceProvider.GetRequiredService<ICodeService<FileInfo, int>>();
            var result = await codeService.CompileAsync(new FileInfo(Path.Combine("scripts", "test.fscript")), new FileInfo(Path.Combine("DeviceInfo", "test.json")));
            Console.WriteLine($"FSharp return value is {result}");
        }

        private static async Task Python(ServiceProvider serviceProvider)
        {
            var codeService = serviceProvider.GetRequiredService<ICodeService<FileInfo, int>>();
            var result = await codeService.CompileAsync(new FileInfo(Path.Combine("scripts", "test.pyscript")), new FileInfo(Path.Combine("DeviceInfo", "test.json")));
            Console.WriteLine($"Python return value is {result}");
        }

        private static async Task JavaScript(ServiceProvider serviceProvider)
        {
            var codeService = serviceProvider.GetRequiredService<ICodeService<FileInfo, int>>();
            var result = await codeService.CompileAsync(new FileInfo(Path.Combine("scripts", "test.jscript")), new FileInfo(Path.Combine("DeviceInfo", "test.json")));
            Console.WriteLine($"JavaScript return value is {result}");
        }
    }
}
