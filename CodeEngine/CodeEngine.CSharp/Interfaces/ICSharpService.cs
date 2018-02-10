using System.Threading.Tasks;

namespace CodeEngine.CSharp.Interfaces
{
    public interface ICSharpService<T>
    {
        Task<T> ExecuteAsync(string code, string deviceState);
    }
}
