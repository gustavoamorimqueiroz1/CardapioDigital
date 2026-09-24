using CompreAqui.Domain.Commands.Inputs;

namespace CompreAqui.Domain.Contracts
{
    public interface IEmailSender
    {
        void SendEmail(EmailMessageCommand message);
    }
}
