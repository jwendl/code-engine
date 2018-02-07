using CodeEngine.Python.Interfaces;
using System.Threading.Tasks;
using IronPythonHosting = IronPython.Hosting.Python;

namespace CodeEngine.Python
{
    public class PythonService<T>
        : IPythonService<T>
    {
        public async Task<T> ExecuteAsync(string code)
        {
            var ironPython = IronPythonHosting.CreateEngine();
            var result = ironPython.Execute(code);
            return await Task.FromResult(result);
        }
    }
}
