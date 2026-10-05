using CompreAqui.Domain.Contracts;

namespace CompreAqui.Domain.Commands.Inputs.UserCommands
{
    public class GetUserByUserNameCommand : ICommand
    {
        public string UserName { get; set; }
    }
}
