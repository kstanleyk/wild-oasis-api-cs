using System;

namespace Core.Common.Contracts;

public interface IChangeLogBase : IIdentifiableEntity
{
    DateTime LastMod { get; set; }
    string ModComputer { get; set; }
    string ModMac { get; set; }
    DateTime ModSystemDate { get; set; }
    string ModUsr { get; set; }
}