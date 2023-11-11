using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WildOasis.API.Models;

public class PatientSearchQueryParameters
{
    [BindRequired]
    public string Branch { get; set; }
    [BindRequired]
    public string PatientName { get; set; }
}