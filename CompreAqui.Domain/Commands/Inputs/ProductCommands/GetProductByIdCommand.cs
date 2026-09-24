using CompreAqui.Domain.Contracts;

namespace CompreAqui.Domain.Commands.Inputs.ProductCommands
{
    public class GetProductByIdCommand : ICommand
    {
        public int Id { get; set; }
    }
}
