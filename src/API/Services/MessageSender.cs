using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WildOasis.API.Services;

public class MessageSender: IMessageSender
{
    public MessageSender(IConfiguration configuration)
    {
        _phoneNumber = configuration["MessageSettings:PhoneNumber"];
        _password = configuration["MessageSettings:Password"];
        _senderId = configuration["MessageSettings:SenderId"];
    }

    private readonly string _phoneNumber;
    private readonly string _password;
    private string _senderId;

    private const string SystemSenderId = "Crestacle";

    public async Task<bool> SendTransactionMessageAsync(string destination, string body, string reference) =>
        await ProcessMessageAsync(destination, body, reference);

    public async Task<bool> SendSystemMessageAsync(string destination, string body, string reference)
    {
        _senderId = SystemSenderId;
        return await ProcessMessageAsync(destination, body, reference);
    }

    private async Task<bool> ProcessMessageAsync(string destination, string body, string reference)
    {
        try
        {
            var url =
                $"sendsms?version=2&phone={_phoneNumber}&password={_password}&from={_senderId}&to={destination}&text={body}&id={reference}";

            var result = await ProcessMessageAsync(url);

            return result;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static async Task<bool> ProcessMessageAsync(string url)
    {
        try
        {
            var client = GetClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode) await response.Content.ReadAsStringAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static HttpClient GetClient()
    {
        var client = new HttpClient
        {
            BaseAddress = new Uri("http://mmp.gtsnetwork.cloud/gts/")
        };
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        return client;
    }
}