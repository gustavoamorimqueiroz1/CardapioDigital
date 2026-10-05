using CompreAqui.Domain.Contracts;
using System;

namespace CompreAqui.Domain.Commands.Inputs.UserAddressCommands
{
    public class GetUserAddressByUserGuidCommand : ICommand
    {
        public Guid UserGuid { get; set; }
    }
}
