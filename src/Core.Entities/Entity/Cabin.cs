using Core.Common.Contracts;

namespace WildOasis.Domain.Entity;

public class Cabin : IIdentifiableEntity
{
    public string Id { get; set; }
    public string Name { get; set; }
    public double MaxCapacity { get; set; }
    public double Discount { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
}