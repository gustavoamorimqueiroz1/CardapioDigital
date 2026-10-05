using CompreAqui.Domain.Contracts;
using System;
namespace CompreAqui.Domain.Commands.Inputs.OrderCommands
{
    public class UpdateOrderStatus : ICommand
    {
        public Guid Guid { get; set; }
        public int Status { get; set; }
    }
}
