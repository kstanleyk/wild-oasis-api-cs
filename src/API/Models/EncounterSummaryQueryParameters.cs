using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WildOasis.API.Models;

public class EncounterSummaryQueryParameters
{
    [BindRequired]
    public string Branch { get; set; }
    [BindRequired]
    public string PatientCode { get; set; }
}