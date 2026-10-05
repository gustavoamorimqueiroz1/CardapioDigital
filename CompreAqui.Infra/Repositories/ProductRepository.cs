using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Models.Entities;
using CompreAqui.Infra.Context;
using CompreAqui.Infra.Contracts;

namespace CompreAqui.Infra.Repositories
{
    public class ProductRepository : DapperRepository<Product>, IRepository
    {
        public ProductRepository(IAppSettings appSettings) : base(appSettings)
        {
        }
    }
}
