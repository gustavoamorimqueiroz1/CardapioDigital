using CompreAqui.Domain.Contracts;

namespace CompreAqui.Domain.Commands.Inputs.UserCommands
{
    public class GetEmailLoginCommand : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
