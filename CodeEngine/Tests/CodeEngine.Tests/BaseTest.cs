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

namespace CodeEngine.Tests
{
    public abstract class BaseTest
    {
        protected readonly IServiceProvider serviceProvider;

        public BaseTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<ICSharpService<string>, CSharpService<string>>();
            serviceCollection.AddScoped<ICodeService<FileInfo, string>, CodeService<FileInfo, string>>();
            serviceCollection.AddScoped<IFSharpService<string>, FSharpService<string>>();
            serviceCollection.AddScoped<IPythonService<string>, PythonService<string>>();
            serviceCollection.AddScoped<IJavaScriptService<string>, JavaScriptService<string>>();
            serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
