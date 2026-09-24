using CompreAqui.Domain.Models.Entities;
using Dapper.FluentMap.Dommel.Mapping;

namespace CompreAqui.Infra.Entities.Mapping
{
    public class CategoryProductMapper : DommelEntityMap<CategoryProduct>, IMapper
    {
        public CategoryProductMapper()
        {
            ToTable("categories");
            Map(x => x.Id).ToColumn("id").IsKey();
            Map(x => x.Guid).ToColumn("guid");
            Map(x => x.CategoryName).ToColumn("categoryname");
            Map(x => x.ImageUrl).ToColumn("imageurl");
            Map(x => x.LastAccess).ToColumn("lastacess");
            Map(x => x.CreatedAt).ToColumn("createdat");
            Map(x => x.UpdatedAt).ToColumn("updatedat");
            Map(x => x.DeletedAt).ToColumn("deletedat");
        }
    }
}
