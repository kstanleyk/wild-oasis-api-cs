using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WildOasis.API.Models;

public class PageParameters
{
    [BindRequired] public int Page { get; set; } = 0;
    [BindRequired] public int Size { get; set; } = 0;
}