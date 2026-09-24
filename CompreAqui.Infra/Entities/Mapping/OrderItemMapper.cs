using CompreAqui.Domain.Models.Entities;
using Dapper.FluentMap.Dommel.Mapping;

namespace CompreAqui.Infra.Entities.Mapping
{
    public class OrderItemMapper : DommelEntityMap<OrderItem>, IMapper
    {
        public OrderItemMapper()
        {
            ToTable("orderitems");
            Map(x => x.Id).ToColumn("id").IsKey();
            Map(x => x.Guid).ToColumn("guid");
            Map(x => x.OrderId).ToColumn("orderid");
            Map(x => x.ProductId).ToColumn("productid");
            Map(x => x.Quantity).ToColumn("quantity");
            Map(x => x.Price).ToColumn("price");
            Map(x => x.Description).ToColumn("productdescription");
            Map(x => x.Unity).ToColumn("unity");
        }
    }

    
}
