using CompreAqui.Domain.Contracts;
using System;
namespace CompreAqui.Domain.Commands.Inputs.ProductCommands
{
    public class GetProductByGuidCommand : ICommand
    {
        public Guid Guid { get; set; }
    }
}
