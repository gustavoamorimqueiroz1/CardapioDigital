using CompreAqui.Domain.Models.Entities;
using Dapper.FluentMap.Dommel.Mapping;

namespace CompreAqui.Infra.Entities.Mapping
{
    public class UserAddressMapper : DommelEntityMap<UserAddress>, IMapper
    {
        public UserAddressMapper()
        {
            ToTable("useraddress");
            Map(x => x.Id).ToColumn("id").IsKey();
            Map(x => x.Guid).ToColumn("guid");
            Map(x => x.UserId).ToColumn("userid");
            Map(x => x.MainAddress).ToColumn("mainaddress");
            Map(x => x.ZipCode).ToColumn("zipcode");
            Map(x => x.Street).ToColumn("street");
            Map(x => x.Number).ToColumn("number");
            Map(x => x.Neighborhood).ToColumn("neighborhood");
            Map(x => x.District).ToColumn("district");
            Map(x => x.City).ToColumn("city");
            Map(x => x.State).ToColumn("state");
            Map(x => x.Complement).ToColumn("complement");
            Map(x => x.Reference).ToColumn("reference");
        }
    }
}
