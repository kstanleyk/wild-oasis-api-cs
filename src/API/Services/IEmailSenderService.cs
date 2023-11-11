using System.Threading.Tasks;

namespace WildOasis.API.Services;

public interface IEmailSenderService
{
    Task SendAsync(string email, string message, string subject);
}