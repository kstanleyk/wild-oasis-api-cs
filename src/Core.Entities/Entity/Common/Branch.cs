using System;
using Core.Common.Contracts;

namespace WildOasis.Domain.Entity.Common;

public class Branch : ITenant
{
    public string Tenant { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string BranchName { get; set; }
    public string BranchShortName { get; set; }
    public string StationCode { get; set; }
    public string Address { get; set; }
    public string Telephone { get; set; }
    public string Region { get; set; }
    public string Motto { get; set; }
    public string HeadOffice { get; set; }
    public string Employer { get; set; }
    public string BranchType { get; set; }
    public string BranchTown { get; set; }
    public DateTime CreatedOn { get; set; }
}