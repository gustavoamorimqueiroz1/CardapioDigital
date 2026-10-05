using CompreAqui.Domain.Contracts;
using System;
namespace CompreAqui.Domain.Commands.Inputs.CustomerCommands
{
    public class GetCustomerByGuidCommand : ICommand
    {
        public Guid Guid { get; set; }
    }
}
