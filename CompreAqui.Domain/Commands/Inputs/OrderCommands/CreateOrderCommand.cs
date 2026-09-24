using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Models.Entities;
using System;
using System.Collections.Generic;

namespace CompreAqui.Domain.Commands.Inputs.OrderCommands
{
    public class CreateOrderCommand : ICommand
    {
        public int UserId { get; set; }

        public int CustomerId { get; set; }

        ///public DateTime Date { get; set; }

        public decimal Total { get; set; }

        public int Status { get; set; }

        public string Description { get; set; }

        public bool CreditCard { get; set; }

        public int UserAddressId { get; set; }

        public bool CatchInStore { get; set; }

        public List<OrderItem> OrderItemList { get; set; }
    }
}
