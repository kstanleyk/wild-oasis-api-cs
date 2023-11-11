namespace WildOasis.API.Core;

public class Helpers
{
    private static bool IsValidEmail(string email)
    {
        try
        {
            var emailAddress = new System.Net.Mail.MailAddress(email);
            return emailAddress.Address == email;
        }
        catch
        {
            return false;
        }
    }
}