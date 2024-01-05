using Core.Common.Contracts;

namespace WildOasis.Domain.Entity;

public class Customer : IIdentifiableEntity
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Nationality { get; set; }
    public string NationalId { get; set; }
    public string CountryFlag { get; set; }
}