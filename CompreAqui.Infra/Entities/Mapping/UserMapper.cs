using CompreAqui.Domain.Models.Entities;
using Dapper.FluentMap.Dommel.Mapping;

namespace CompreAqui.Infra.Entities.Mapping
{
    public class UserMapper : DommelEntityMap<User>, IMapper
    {
        public UserMapper()
        {
            ToTable("users");
            Map(x => x.Id).ToColumn("id").IsKey();
            Map(x => x.Guid).ToColumn("guid");
            Map(x => x.UserName).ToColumn("username");
            Map(x => x.Email).ToColumn("email");
            Map(x => x.Password).ToColumn("password");
            Map(x => x.FirstName).ToColumn("firstname");
            Map(x => x.LastName).ToColumn("lastname");
            Map(x => x.FacebookId).ToColumn("facebookid");
            Map(x => x.PhoneNumber).ToColumn("phonenumber");
            Map(x => x.Rating).ToColumn("rating");
            Map(x => x.LastAcess).ToColumn("lastacess");
            Map(x => x.CreatedAt).ToColumn("createdat");
            Map(x => x.UpdatedAt).ToColumn("updatedat");
            Map(x => x.DeletedAt).ToColumn("deletedat");
        }

    }
}