using System;
using System.Collections.Generic;
using System.Text;

namespace CompreAqui.Domain.Commands.Inputs.ImageCommands
{
    public class GetBase64ImageCommand
    {
        public string Image { get; set; }
        public string Container { get; set; }
    }
}
