using CompreAqui.Domain.Attributes;

namespace CompreAqui.Domain.Models.Entities
{

    [Entity("orderitems", "System orders", true)]
    public class OrderItem : EntityModels
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Unity { get; set; }
    }
}
