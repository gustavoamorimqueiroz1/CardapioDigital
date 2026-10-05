using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Models.Settings;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CompreAqui.Infra.Context
{
    public abstract class DapperRepository<TEntity> : IDapperRepository<TEntity>, IDisposable where TEntity : class, IEntity
    {
        //protected readonly NpgsqlConnection Connection;
        protected readonly SqlConnection Connection;
        //protected readonly ConnectionBd ConnectionBd;
        protected readonly ConnectionStringSettings ConnectionString;

        protected DapperRepository(IAppSettings appSettings)
        {
            //ConnectionBd = appSettings.GetConnectionBd();
            ConnectionString = appSettings.GetConnectionStringSettings();
            try
            {
                //Connection = new SqlConnection(appSettings.GetStringConnection());
                Connection = new SqlConnection(appSettings.GetStringConnection());
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<TEntity> Add(string sql)
        {
            if (string.IsNullOrEmpty(sql)) throw new ArgumentNullException("sql não pode ser nulo");

            try
            {
                var returnList = await Connection.QueryAsync(sql);// as TEntity;
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar executar insert. Sql: {sql}. Erro: {ex.Message} ");
            }
        }


        public void Dispose()
        {
            Connection.Dispose();
            this.Dispose();
        }

       

        public async Task<List<TEntity>> GetAll(string sql)
        {
            try
            {
                var retorno = await Connection.QueryAsync(sql);// GetAllAsync<TEntity>();
                return (List<TEntity>)retorno;
            }
            catch (Exception exception)
            {
                throw new Exception($"Erro ao tentar trazer todos os bancos dos dados da tabela {typeof(TEntity).Name}", exception);
            }

        }



        public async Task<TEntity> GetById(string sql)
        {
            try
            {
                var retorno = await Connection.QueryAsync(sql);
                return (TEntity)retorno;

            }
            catch (Exception exception)
            {
                throw new Exception($"Erro ao tentar Recuperar dados pelo ID da tabela {typeof(TEntity).Name}", exception);
            }
        }

        public async Task<TEntity> GetOneBySql(string sql)
        {
            try
            {
                return await Connection.QuerySingleOrDefaultAsync<TEntity>(sql);
            }
            catch (Exception exception)
            {

                throw new ArgumentException($"Erro ao tentar processar consulta por filtro SQL na entidade: Executando Sql: {sql}{Environment.NewLine}{typeof(TEntity)}", exception);
            }
        }


        public async Task<TEntity> GetFirstOneBySql(string sql)
        {
            try
            {
                return await Connection.QueryFirstOrDefaultAsync<TEntity>(sql);
            }
            catch (Exception exception)
            {

                throw new ArgumentException($"Erro ao tentar processar consulta por filtro SQL na entidade: Executando Sql: {sql}{Environment.NewLine}{typeof(TEntity)}", exception);
            }
        }

        public async Task<List<T>> GetListBySql<T>(string sql)
        {
            try
            {
                if (string.IsNullOrEmpty(sql)) throw new ArgumentNullException($"sql Não pode ser nula");
                var returnList = await Connection.QueryAsync<T>(sql);
                return returnList.ToList();

            }
            catch (Exception exception)
            {

                throw new ArgumentException($"Erro ao tentar processar consulta por filtro na entidade: {typeof(T)}", exception);
            }
        }

        public IDbConnection GetConnection()
        {
            return Connection;
        }


        public async Task<bool> InsertBySql(string sql)
        {
            if (string.IsNullOrEmpty(sql)) throw new ArgumentNullException("Sql não pode ser nula");
            try
            {
                var returnList = await Connection.QueryAsync(sql);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar executar insert. Sql: {sql}. Erro: {ex.Message}");
            }
        }

        public bool TransactionEntityListBySqlList(List<string> listSql)
        {
            Connection.Open();
            SqlTransaction transaction = Connection.BeginTransaction();
            try
            {
                foreach (var sql in listSql)
                {
                    SqlCommand cmd = new SqlCommand(sql, Connection, transaction);
                    var execute = cmd.ExecuteNonQuery();
                }

                transaction.Commit();
                Connection.Close();
                return true;
            }
            catch (Exception exception)
            {
                // log exception
                try
                {
                    transaction.Rollback();
                    Connection.Close();
                    throw new Exception($"Erro ao tentar realizar a transação ", exception);
                }
                catch (Exception ex2)
                {
                    throw new Exception($"Erro ao tentar dar rollback na transação ", ex2);
                }
            }

        }

        public async Task<List<IEntity>> InsertListBySql<IEntity>(string sql)
        {
            if (string.IsNullOrEmpty(sql)) throw new ArgumentNullException("Sql não pode ser nula");

            try
            {
                var returnList = await Connection.QueryAsync<IEntity>(sql);
                return returnList.ToList();
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao tentar executar insert list. Sql: {sql}. Erro: {ex.Message}");
            }
        }

        public async Task<bool> UpdateBySql(string sql)
        {
            if (string.IsNullOrEmpty(sql)) throw new ArgumentNullException("Sql não pode ser nula");

            try
            {
                var returnList = await Connection.QueryAsync(sql);
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao tentar executar update. Sql: {sql}. Erro: {ex.Message}");
            }
        }

        public async Task<List<IEntity>> DeleteListBySql<IEntity>(string sql)
        {
            if (string.IsNullOrEmpty(sql)) throw new ArgumentNullException("Sql não pode ser nula");

            try
            {
                var returnList = await Connection.QueryAsync<IEntity>(sql);
                return returnList.ToList();
            }
            catch (Exception)
            {

                throw new Exception($"Erro ao tentar executar delete. Sql: {sql}");
            }
        }
        public async Task<bool> DeleteBySql(string sql)
        {
            if (string.IsNullOrEmpty(sql)) throw new ArgumentNullException("Sql não pode ser nula");

            try
            {
                var returnList = await Connection.QueryAsync(sql);
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao tentar executar delete. Sql: {sql}. Erro: {ex.Message}");
            }
        }

        public Task<List<TEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}