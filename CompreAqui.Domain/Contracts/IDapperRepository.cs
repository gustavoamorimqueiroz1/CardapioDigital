using CompreAqui.Domain.Contracts;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CompreAqui.Domain.Contracts
{
    public interface IDapperRepository<TEntity> where TEntity : class, IEntity
    {
        Task<bool> InsertBySql(string sql);
        Task<bool> UpdateBySql(string sql);
        Task<bool> DeleteBySql(string sql);
        Task<TEntity> GetOneBySql(string sql);
        Task<List<T>> GetListBySql<T>(string sql);
        Task<List<IEntity>> InsertListBySql<IEntity>(string sql);
        Task<List<IEntity>> DeleteListBySql<IEntity>(string sql);
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        IDbConnection GetConnection();
        bool TransactionEntityListBySqlList(List<string> listSql);
    }
}