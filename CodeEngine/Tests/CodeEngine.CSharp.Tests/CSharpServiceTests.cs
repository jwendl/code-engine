using CodeEngine.CSharp.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CodeEngine.CSharp.Tests
{
    public class CSharpServiceTests
    {
        private readonly IServiceProvider serviceProvider;

        public CSharpServiceTests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<ICSharpService<string>, CSharpService<string>>();
            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [Fact]
        public async Task CanExecuteAsync()
        {
            var csharpService = serviceProvider.GetRequiredService<ICSharpService<string>>();
            var json = @"{
    ""firstName"": ""John"",
    ""lastName"": ""Smith"",
}";

            var code = $@"
var initialState = InitialState;
return initialState;
";

            var result = await csharpService.ExecuteAsync(code, json);
            Assert.Equal(json, result);
        }
    }
}
