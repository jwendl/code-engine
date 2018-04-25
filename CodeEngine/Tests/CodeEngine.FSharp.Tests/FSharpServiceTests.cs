using CodeEngine.FSharp.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CodeEngine.FSharp.Tests
{
    public class FSharpServiceTests
    {
        private readonly IServiceProvider serviceProvider;

        public FSharpServiceTests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IFSharpService<string>, FSharpService<string>>();
            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [Fact]
        public async Task CanExecuteAsync()
        {
            var fsharpService = serviceProvider.GetRequiredService<IFSharpService<string>>();
            var json = @"{
    ""firstName"": ""John"",
    ""lastName"": ""Smith"",
}";

            var code = $@"42+1";

            var result = await fsharpService.ExecuteAsync(code, json);
            Assert.Equal(json, result);
        }
    }
}
