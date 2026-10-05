using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Models.Entities;
using CompreAqui.Infra.Context;
using CompreAqui.Infra.Contracts;

namespace CompreAqui.Infra.Repositories
{
    public class OrderRepository : DapperRepository<Order>, IRepository
    {
        public OrderRepository(IAppSettings appSettings) : base(appSettings)
        {
        }
    }
}
