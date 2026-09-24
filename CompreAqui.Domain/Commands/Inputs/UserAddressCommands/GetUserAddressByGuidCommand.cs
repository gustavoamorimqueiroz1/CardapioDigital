using CompreAqui.Domain.Contracts;
using System;

namespace CompreAqui.Domain.Commands.Inputs.UserAddressCommands
{
    public class GetUserAddressByGuidCommand : ICommand
    {
        public Guid Guid { get; set; }
    }
}
