using CompreAqui.Domain.Models.Entities;
using Dapper.FluentMap.Dommel.Mapping;

namespace CompreAqui.Infra.Entities.Mapping
{
    class ProductMapper : DommelEntityMap<Product>, IMapper
    {
        public ProductMapper()
        {
            ToTable("products");
            Map(x => x.Id).ToColumn("id").IsKey();
            Map(x => x.Guid).ToColumn("guid");
            Map(x => x.CategoryId).ToColumn("categoryid");
            Map(x => x.CustomerId).ToColumn("customerid");
            Map(x => x.ProductName).ToColumn("productname");
            Map(x => x.Image).ToColumn("image");
            Map(x => x.Active).ToColumn("active");
            Map(x => x.Fractionated).ToColumn("fractionated");
            Map(x => x.Price).ToColumn("price");
            Map(x => x.PromotionalPrice).ToColumn("promotionalprice");
            Map(x => x.OnSale).ToColumn("onsale");
            Map(x => x.Unity).ToColumn("unity");
            Map(x => x.LastAccess).ToColumn("lastacess");
            Map(x => x.CreatedAt).ToColumn("createdat");
            Map(x => x.UpdatedAt).ToColumn("updatedat");
            Map(x => x.DeletedAt).ToColumn("deletedat");
        }
    }
}
