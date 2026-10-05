using CompreAqui.Domain.Contracts;

namespace CompreAqui.Domain.Commands.Inputs.UserCommands
{
    public class GetUserNameLoginCommand : ICommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
