using CompreAqui.Domain.Contracts;
using System;

namespace CompreAqui.Domain.Commands.Inputs.UserCommands
{
    public class GetUserByGuidCommand : ICommand
    {
        public Guid Guid { get; set; }
    }
}
