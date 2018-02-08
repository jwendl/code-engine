using CodeEngine.JavaScript.Interfaces;
using System.Threading.Tasks;

namespace CodeEngine.JavaScript
{
    public class JavaScriptService<T>
        : IJavaScriptService<T>
    {
        public async Task<T> ExecuteAsync(string code)
        {
            var engine = new Jurassic.ScriptEngine();
            var result = engine.Evaluate<T>(code);
            return await Task.FromResult(result);
        }
    }
}
