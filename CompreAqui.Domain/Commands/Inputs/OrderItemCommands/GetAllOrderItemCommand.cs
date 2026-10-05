using CompreAqui.Domain.Contracts;

namespace CompreAqui.Domain.Commands.Inputs.OrderItemCommands
{
    public class GetAllOrderItemCommand : ICommand
    {
        public int OrderId { get; set; }
    }
}
