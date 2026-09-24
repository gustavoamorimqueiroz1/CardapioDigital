using CompreAqui.Domain.Models.Entities;
using Dapper.FluentMap.Dommel.Mapping;


namespace CompreAqui.Infra.Entities.Mapping
{
    public class OrderMapper : DommelEntityMap<Order>, IMapper
    {
        public OrderMapper()
        {
            ToTable("orders");
            Map(x => x.Id).ToColumn("id").IsKey();
            Map(x => x.Guid).ToColumn("guid");
            Map(x => x.UserId).ToColumn("userid");
            Map(x => x.CustomerId).ToColumn("customerid");
            Map(x => x.Date).ToColumn("date");
            Map(x => x.Total).ToColumn("total");
            Map(x => x.Status).ToColumn("status");
            Map(x => x.Description).ToColumn("description");
            Map(x => x.CreditCard).ToColumn("creditcard");
            Map(x => x.UserAddressId).ToColumn("useraddressid");
            Map(x => x.Message).ToColumn("messages");
            Map(x => x.CustomerViewed).ToColumn("customerviewed");
            Map(x => x.UserViewed).ToColumn("userviewed");
            Map(x => x.CatchInStore).ToColumn("catchinstore");

        }
    }
}
