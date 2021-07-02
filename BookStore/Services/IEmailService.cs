using BookStore.Models;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);
        Task SendEmailConfirmationMail(UserEmailOptions userEmailOptions);
        Task SendForgotPasswordMail(UserEmailOptions userEmailOptions);
    }
}