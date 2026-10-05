using System;
using System.Collections.Generic;
using System.Text;

namespace CompreAqui.Domain.Commands.Inputs.OrderCommands
{
    public class UpdateOrderUserViewedCommand
    {
        public int Id { get; set; }
        public bool UserViewed { get; set; }
    }
}
