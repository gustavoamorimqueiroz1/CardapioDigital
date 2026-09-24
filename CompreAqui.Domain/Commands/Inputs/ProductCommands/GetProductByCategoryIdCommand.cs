using CompreAqui.Domain.Contracts;

namespace CompreAqui.Domain.Commands.Inputs.ProductCommands
{
    public class GetProductByCategoryIdCommand : ICommand
    {
        public int CategoryId { get; set; }
        public int CustomerId { get; set; }
    }
}
