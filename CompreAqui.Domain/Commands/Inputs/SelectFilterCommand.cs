using CompreAqui.Domain.Contracts;

namespace CompreAqui.Domain.Commands.Inputs
{
    public class SelectFilterCommand : ICommand
    {
        public object Field { get; set; }
        public object Value { get; set; }
        public int CustomerId { get; set; }
    }
}
