using CompreAqui.Domain.Attributes;
using System;

namespace CompreAqui.Domain.Models.Entities
{
    [Entity("products", "System products", true)]
    public class Product : EntityModels
    {
        public int CategoryId { get; set; }
        public int CustomerId { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public bool Active { get; set; }
        public bool Fractionated { get; set; }
        public decimal Price { get; set; }
        public decimal PromotionalPrice { get; set; }
        public bool OnSale { get; set; }
        public string Unity { get; set; }
        public DateTime LastAccess { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
