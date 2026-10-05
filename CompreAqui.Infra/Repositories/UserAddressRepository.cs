using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Models.Entities;
using CompreAqui.Infra.Context;
using CompreAqui.Infra.Contracts;

namespace CompreAqui.Infra.Repositories
{
    public class UserAddressRepository : DapperRepository<UserAddress>, IRepository
    {
        public UserAddressRepository(IAppSettings appSettings) : base(appSettings)
        {
        }

    }
}
