
using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Models.Entities;
using CompreAqui.Infra.Context;
using CompreAqui.Infra.Contracts;

namespace CompreAqui.Infra.Repositories
{
    public class CategoryProductRepository : DapperRepository<CategoryProduct>, IRepository
    {
        public CategoryProductRepository(IAppSettings appSettings) : base(appSettings)
        {

        }
    }
}
