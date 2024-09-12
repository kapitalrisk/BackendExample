using System.Threading.Tasks;

namespace InMemoryDatabase
{
    public interface IDefaultRepository
    {
        int Execute(string sql);
        Task<int> ExecuteAsync(string sql);
    }
}
