using System.Threading.Tasks;

namespace CodeEngine.JavaScript.Interfaces
{
    public interface IJavaScriptService<T>
    {
        Task<T> ExecuteAsync(string code);
    }
}
