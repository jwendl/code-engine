using CodeEngine.JavaScript.Interfaces;
using Jint;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CodeEngine.JavaScript
{
    public class JavaScriptService<T>
        : IJavaScriptService<T>
    {
        public async Task<T> ExecuteAsync(string code)
        {
            var engine = new Engine();
            engine.SetValue("log", new Action<object>(Console.WriteLine));
            var json = engine.Execute(code)
                .GetCompletionValue()
                .ToObject();

            var result = JsonConvert.DeserializeObject<T>(json.ToString());
            return await Task.FromResult(result);
        }
    }
}
