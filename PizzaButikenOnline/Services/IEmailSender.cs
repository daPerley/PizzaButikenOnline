using System.Threading.Tasks;

namespace PizzaButikenOnline.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
