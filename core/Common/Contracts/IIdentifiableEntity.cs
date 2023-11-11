namespace Core.Common.Contracts;

public interface IIdentifiableEntity { }

public interface ITenant : IIdentifiableEntity
{
    string Tenant { get; set; }
}