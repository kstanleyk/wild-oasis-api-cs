using System.Threading.Tasks;

namespace WildOasis.API.Services;

public interface IMessageSender
{
    Task<bool> SendTransactionMessageAsync(string destination, string body, string reference);
    Task<bool> SendSystemMessageAsync(string destination, string body, string reference);
}