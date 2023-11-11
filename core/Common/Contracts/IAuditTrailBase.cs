using System;

namespace Core.Common.Contracts;

public interface IAuditTrailBase : IIdentifiableEntity
{
    string Tenant { get; set; }
    string AddComputer { get; set; }
    string AddMac { get; set; }
    DateTime AddSystemDate { get; set; }
    string AddUsr { get; set; }
    DateTime LastMod { get; set; }
    string ModComputer { get; set; }
    string ModMac { get; set; }
    DateTime ModSystemDate { get; set; }
    string ModUsr { get; set; }
}