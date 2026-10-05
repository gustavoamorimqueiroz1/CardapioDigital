namespace CompreAqui.Domain.Commands.Inputs
{
    public class ResetPasswordCommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
