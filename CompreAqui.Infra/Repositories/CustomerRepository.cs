using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Models.Entities;
using CompreAqui.Infra.Context;
using CompreAqui.Infra.Contracts;

namespace CompreAqui.Infra.Repositories
{
    public class CustomerRepository : DapperRepository<Customer>, IRepository
    {
        public CustomerRepository(IAppSettings appSettings) : base(appSettings)
        {
        }

    }
}
