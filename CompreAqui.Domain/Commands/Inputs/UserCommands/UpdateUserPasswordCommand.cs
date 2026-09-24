using CompreAqui.Domain.Contracts;
using System;

namespace CompreAqui.Domain.Commands.Inputs.UserCommands
{
    public class UpdateUserPasswordCommand : ICommand
    {
        public string Guid { get; set; }
        public string Password { get; set; }
    }
}
