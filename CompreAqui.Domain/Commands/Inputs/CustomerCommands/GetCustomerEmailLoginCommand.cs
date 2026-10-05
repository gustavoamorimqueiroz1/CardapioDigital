using CompreAqui.Domain.Contracts;
namespace CompreAqui.Domain.Commands.Inputs.CustomerCommands
{
    public class GetCustomerEmailLoginCommand : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
