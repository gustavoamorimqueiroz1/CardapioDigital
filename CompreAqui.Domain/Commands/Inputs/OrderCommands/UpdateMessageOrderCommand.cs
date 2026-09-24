using System;
using System.Collections.Generic;
using System.Text;

namespace CompreAqui.Domain.Commands.Inputs.OrderCommands
{
    public class UpdateMessageOrderCommand
    {
        public int Id { get; set; }
        public string Message { get; set; }
    }
}
