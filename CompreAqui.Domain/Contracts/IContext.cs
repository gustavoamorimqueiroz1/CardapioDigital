using CompreAqui.Domain.Models.Entities;

namespace CompreAqui.Domain.Contracts
{
    public interface IContext
    {
        IDapperRepository<User> Users { get; set; }
        IDapperRepository<Customer> Customers { get; set; }
        IDapperRepository<Product> Products { get; set; }
        IDapperRepository<CategoryProduct> Categories { get; set; }
        IDapperRepository<UserAddress> UserAddresses { get; set; }
        IDapperRepository<Order> Orders { get; set; }
        IDapperRepository<OrderItem> OrderItems { get; set; }

    }
}
