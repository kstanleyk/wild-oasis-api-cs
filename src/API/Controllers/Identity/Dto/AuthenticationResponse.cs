using System;

namespace WildOasis.API.Controllers.Identity.Dto;

public class AuthenticationResponse
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public Boolean Success { get; set; }
    public string ErrorMessage { get; set; }
}