using System.Security.Cryptography;
using System.Text;
using Core.Common.Core;

namespace WildOasis.Application;

public class DomainBase : Disposable
{
    internal static string GetStringSha256Hash(string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        using var sha = SHA256.Create();
        var textData = Encoding.UTF8.GetBytes(text);
        var hash = sha.ComputeHash(textData);
        return BitConverter.ToString(hash).Replace("-", string.Empty);
    }

    protected static string GetCustomerNumberMasked(string input)
    {
        var sb = new StringBuilder(input);
        for (var i = 2; i < 8; i++) sb[i] = 'X';

        return sb.ToString().Substring(sb.Length - 5, 5);
    }
}