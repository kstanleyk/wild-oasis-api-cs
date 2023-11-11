using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Core;
using Core.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UAParser;
using WildOasis.Domain.Vm;

namespace WildOasis.API.Core;

[Route("api/[controller]")]
[ApiController]
public class ApiControllerBase : ControllerBase
{
    protected async Task<IActionResult> GetActionResult(Func<Task<IActionResult>> codeToExecute)
    {
        return await codeToExecute.Invoke();
    }

    private string GetUserAgent()
    {
        var request = HttpContext.Request;

        return request.Headers["User-Agent"];
    }

    private void UserAgent()
    {
        //string uaString = "Mozilla/5.0 (iPhone; CPU iPhone OS 5_1_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9B206 Safari/7534.48.3";
        string uaString = GetUserAgent();

        // get a parser with the embedded regex patterns
        var uaParser = Parser.GetDefault();

        // get a parser using externally supplied yaml definitions
        // var uaParser = Parser.FromYaml(yamlString);

        var c = uaParser.Parse(uaString);

        Console.WriteLine(c.UA.Family); // => "Mobile Safari"
        Console.WriteLine(c.UA.Major);  // => "5"
        Console.WriteLine(c.UA.Minor);  // => "1"

        Console.WriteLine(c.OS.Family);        // => "iOS"
        Console.WriteLine(c.OS.Major);         // => "5"
        Console.WriteLine(c.OS.Minor);         // => "1"

        Console.WriteLine(c.Device.Family);    // => "iPhone"

    }

    protected async Task<IActionResult> GetActionResult(string requiredClaim, Func<Task<IActionResult>> codeToExecute)
    {
        //try
        //{
        //    return await codeToExecute.Invoke();
        //}
        //catch (ValidationException ex)
        //{
        //    var validationErrors = ex.ValidationErrors;
        //    var i = 1;
        //    foreach (var validationError in validationErrors)
        //    {
        //        ModelState.AddModelError(i.ToString(),validationError);
        //        i++;
        //    }
        //    return ValidationProblem();
        //}
        return await codeToExecute.Invoke();
    }

    protected CurrentUserVm GetCurrentUser()
    {
        //var currentUser = new CurrentUserVm();
        //var claimsPrincipal = HttpContext.User.Identity as ClaimsIdentity;
        //currentUser.Code = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == "user_map")?.Value;
        //currentUser.Telephone = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == "user_telephone")?.Value;
        //currentUser.Email = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == "user_email")?.Value;
        //currentUser.Tenant = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == "user_organization")?.Value;
        //currentUser.ImageUrl = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == "user_avatar")?.Value;
        //currentUser.Name = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == "user_fullname")?.Value;
        //currentUser.Subject = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        //return currentUser;

        var currentUser = new CurrentUserVm();
        var claimsPrincipal = HttpContext.User.Identity as ClaimsIdentity;
        currentUser.Code = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == "user_map")?.Value;
        currentUser.Telephone = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == "user_telephone")?.Value;
        currentUser.Email = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == "user_email")?.Value;
        currentUser.Tenant = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == "user_organization")?.Value;
        currentUser.ImageUrl = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == "user_avatar")?.Value;
        currentUser.Name = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == "user_fullname")?.Value;
        currentUser.Subject = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        currentUser.UserAgent = GetUserAgent();

        return currentUser;
    }

    protected LogDetail GetLogDetail(string message, Exception ex)
    {
        const string product = "planner-api";
        const string location = "Core API"; // this application
        const string layer = "Core API"; // unattended executable invoked somehow
        var user = Environment.UserName;
        var hostname = Environment.MachineName;

        return new LogDetail
        {
            Product = product,
            Location = location,
            Layer = layer,
            UserName = user,
            Hostname = hostname,
            Message = message,
            Exception = ex
        };
    }

    protected HttpClient GetHttpClient(string address)
    {
        var client = new HttpClient
        {
            BaseAddress = new Uri(address)
        };

        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.ConnectionClose = false;
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        return client;
    }

    private static string SerializeObject(object payLoad)
    {
        var serializedItem = JsonConvert.SerializeObject(payLoad, Formatting.None, new JsonSerializerSettings
        {
            DateTimeZoneHandling = DateTimeZoneHandling.Unspecified,
            NullValueHandling = NullValueHandling.Ignore
        });
        return serializedItem;
    }

    protected bool IsValidEmail(string email)
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

    protected async Task<dynamic> Post(HttpClient client, string url, dynamic entity)
    {
        try
        {
            object result;

            var serializedItemToCreate = SerializeObject(entity);
            var response = await client.PostAsync(url,
                new StringContent(serializedItemToCreate, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<dynamic>(data);
            }
            else
            {
                result = null;
            }

            return result;
        }
        catch (Exception)
        {
            return null;
        }
    }

    protected async Task<dynamic> Get(HttpClient client, string url)
    {
        try
        {
            object result;

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<object>(data);
            }
            else
            {
                result = null;
            }

            return result;
        }
        catch (Exception)
        {
            return null;
        }
    }

    protected IActionResult ReturnValidationProblem(BaseResponse response)
    {
        var i = 0;
        foreach (var error in response.ValidationErrors)
        {
            ModelState.AddModelError(i.ToString(), error);
            i++;
        }

        return ValidationProblem();
    }
}