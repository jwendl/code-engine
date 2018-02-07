using System.Threading.Tasks;

namespace CodeEngine.Python.Interfaces
{
    public interface IPythonService<T>
    {
        Task<T> ExecuteAsync(string code);
    }
}
