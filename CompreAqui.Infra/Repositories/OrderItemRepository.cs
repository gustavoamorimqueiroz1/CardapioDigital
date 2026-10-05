using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Models.Entities;
using CompreAqui.Infra.Context;
using CompreAqui.Infra.Contracts;

namespace CompreAqui.Infra.Repositories
{
    public class OrderItemRepository : DapperRepository<OrderItem>, IRepository
    {
        public OrderItemRepository(IAppSettings appSettings) : base(appSettings)
        {
        }
    }
}
