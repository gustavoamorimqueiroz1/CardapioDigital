using CompreAqui.Domain.Contracts;
using System;

namespace CompreAqui.Domain.Commands.Inputs.UserAddressCommands
{
    public class UpdateUserAddressCommand : ICommand
    {
        public Guid AddressGuid { get; set; }
        public bool MainAddress { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Complement { get; set; }
        public string Reference { get; set; }
    }
}
