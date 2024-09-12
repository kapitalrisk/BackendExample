using System;
using System.Threading.Tasks;

namespace InMemoryDatabase
{
    public interface IDatabaseGenerator
    {
        bool RegisterTableEntity(Type entityType);
        void GenerateDatabase();
        Task GenerateDatabaseAsync();
    }
}
