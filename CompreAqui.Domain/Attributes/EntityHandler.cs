using CompreAqui.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CompreAqui.Domain.Attributes
{
    public class EntityHandler<TEntity> where TEntity : class, IEntity
    {
        protected readonly IDapperRepository<TEntity> Repository;

        protected EntityHandler(IContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            Repository = GetRepository(context);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await Repository.GetAll();
            
        }

        public async Task<TEntity> GetById(int id)
        {
            return await Repository.GetById(id);
        }

        private IDapperRepository<TEntity> GetRepository(IContext context)
        {
            Type entityType = typeof(TEntity);
            PropertyInfo propertyInfo = ((IEnumerable<PropertyInfo>)typeof(IContext).GetProperties()).FirstOrDefault(p => p.PropertyType.GetGenericArguments()[0] == entityType);
            if (propertyInfo == null) throw new ArgumentException($"Erro ao tentar instaciar o contexto {entityType.Name}, eles devem ser criados na IContext e depois implementados", nameof(entityType));
            var prop = (object)propertyInfo;
            var obj = propertyInfo.GetValue(context);

            return (IDapperRepository<TEntity>)propertyInfo.GetValue(context);
        }

    }
}
