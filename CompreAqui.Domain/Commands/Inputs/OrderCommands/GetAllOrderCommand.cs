using CompreAqui.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompreAqui.Domain.Commands.Inputs.OrderCommands
{
    public class GetAllOrderCommand : ICommand
    {
        public int userId { get; set; }
    }
}
