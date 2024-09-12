using Dapper.FastCrud.Configuration.StatementOptions.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InMemoryDatabase
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task InsertAsync(TEntity entity);
        Task<TEntity?> GetAsync(TEntity entity); // return nullable for when nothing is found for input entity keys
        Task<IEnumerable<TEntity>> FindAsync(Action<IRangedBatchSelectSqlSqlStatementOptionsOptionsBuilder<TEntity>>? statementOptions = null);
        Task<IEnumerable<TEntity>> WhereAsync(FormattableString whereClause, object parameters);
        Task<bool> UpdateAsync(TEntity entityToUpdate);
        Task<bool> DeleteAsync(TEntity entityToDelete);
    }
}
