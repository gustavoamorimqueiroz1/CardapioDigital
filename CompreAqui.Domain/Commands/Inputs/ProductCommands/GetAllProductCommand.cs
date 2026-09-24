using CompreAqui.Domain.Contracts;

namespace CompreAqui.Domain.Commands.Inputs.ProductCommands
{
    public class GetAllProductsCommand : ICommand
    {
        public int CustomerId { get; set; }
    }
}
