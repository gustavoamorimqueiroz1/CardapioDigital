using CompreAqui.Domain.Attributes;
using CompreAqui.Domain.Contracts;
using System;
namespace CompreAqui.Domain.Models.Entities
{

    [Entity("orders", "System orders", true)]
    
    public class Order : EntityModels
    {
        public int UserId { get; set; }

        public int CustomerId { get; set; }

        public DateTime Date { get; set; }
        
        public decimal Total { get; set; }

        public int Status { get; set; }

        public string Description { get; set; }

        public bool CreditCard { get; set; }
        
        public int UserAddressId { get; set; }

        public string Message { get; set; }

        public bool CustomerViewed { get; set; }

        public bool UserViewed { get; set; }

        public bool CatchInStore { get; set; }

    }
}
