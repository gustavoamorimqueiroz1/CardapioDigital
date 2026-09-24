using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Models.Entities;
using CompreAqui.Infra.Context;
using CompreAqui.Infra.Contracts;

namespace CompreAqui.Infra.Repositories
{
    public class UserRepository : DapperRepository<User>, IRepository
    {
        public UserRepository(IAppSettings appSettings) : base(appSettings)
        {
        }

    }
}