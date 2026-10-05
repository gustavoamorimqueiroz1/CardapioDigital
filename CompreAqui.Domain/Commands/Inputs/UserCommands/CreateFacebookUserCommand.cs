using CompreAqui.Domain.Contracts;

namespace CompreAqui.Domain.Commands.Inputs.UserCommands
{
    public class CreateFacebookUserCommand : ICommand
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long FacebookId { get; set; }
    }
}
