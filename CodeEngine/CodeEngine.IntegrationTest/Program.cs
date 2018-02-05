using CodeEngine.CSharp;
using CodeEngine.CSharp.Interfaces;
using CodeEngine.Interfaces;
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
            serviceCollection.AddScoped<ICodeService<FileInfo, int>, CodeService<FileInfo, int>>();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var codeService = serviceProvider.GetRequiredService<ICodeService<FileInfo, int>>();
            var result = await codeService.CompileAsync(new FileInfo("test.cscript"));

            foreach (var exception in result.Exceptions)
            {
                Console.WriteLine($"Error: {exception.Message}");
            }
            Console.WriteLine($"Return value is {result.ReturnValue}");
            Console.ReadKey();
        }
    }
}
