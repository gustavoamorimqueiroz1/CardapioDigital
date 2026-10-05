using CompreAqui.Domain.Contracts;
using System;

namespace CompreAqui.Domain.Commands.Inputs.UserCommands
{
    public class UpdateUserCommand : ICommand
    {
        public string Guid { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int Rating { get; set; }
        public string Password { get; set; }
    }
}
