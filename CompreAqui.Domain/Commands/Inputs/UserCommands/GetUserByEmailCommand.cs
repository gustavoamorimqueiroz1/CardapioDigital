using CompreAqui.Domain.Contracts;

namespace CompreAqui.Domain.Commands.Inputs.UserCommands
{
    public class GetUserByEmailCommand : ICommand
    {
        public string Email { get; set; }
    }
}
