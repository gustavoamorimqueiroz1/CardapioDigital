using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Models.Entities;
using CompreAqui.Infra.Entities.Mapping;
using CompreAqui.Infra.Repositories;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using System.Linq;

namespace CompreAqui.Infra.Context
{
    public class DataContext : IContext
    {
        private readonly IAppSettings _settings;
        public IDapperRepository<User> Users { get; set; }
        public IDapperRepository<Customer> Customers { get; set; }
        public IDapperRepository<Product> Products { get; set; }
        public IDapperRepository<CategoryProduct> Categories { get; set; }
        public IDapperRepository<UserAddress> UserAddresses { get; set; }
        public IDapperRepository<Order> Orders { get; set; }
        public IDapperRepository<OrderItem> OrderItems { get; set; }

        public DataContext(IAppSettings appSettings)
        {
            _settings = appSettings;
            this.BuildRepositoriesAsync();
            this.BuildMappers();
        }

        private void BuildRepositoriesAsync()
        {
            //Verificar uma forma de fazer por reflection para evitar que esqueçamos de instanciar algum repositório
            Users = new UserRepository(_settings);
            Customers = new CustomerRepository(_settings);
            Products = new ProductRepository(_settings);
            Categories = new CategoryProductRepository(_settings);
            UserAddresses = new UserAddressRepository(_settings);
            Orders = new OrderRepository(_settings);
            OrderItems = new OrderItemRepository(_settings);
        }

        private void BuildMappers()
        {
            if (!FluentMapper.EntityMaps.Any()) // Passa todos mapeamentos do dapper criados aqui
            {
                FluentMapper.Initialize(
                    config =>
                    {
                        //Verificar uma forma de fazer por reflection para evitar que esqueçamos algum mapeamento
                        config.AddMap(new UserMapper());
                        config.AddMap(new CustomerMapper());
                        config.AddMap(new ProductMapper());
                        config.AddMap(new CategoryProductMapper());
                        config.AddMap(new UserAddressMapper());
                        config.AddMap(new OrderMapper());
                        config.AddMap(new OrderItemMapper());
                        config.ForDommel();
                    });
            }
        }

    }

}