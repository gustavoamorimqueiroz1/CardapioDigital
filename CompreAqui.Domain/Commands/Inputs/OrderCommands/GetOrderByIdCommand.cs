

using CompreAqui.Domain.Contracts;

namespace CompreAqui.Domain.Commands.Inputs.OrderCommands
{
    public class GetOrderByIdCommand : ICommand
    {
        public int Id { get; set; }
    }
}
