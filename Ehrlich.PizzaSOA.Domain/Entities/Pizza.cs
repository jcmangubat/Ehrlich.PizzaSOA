using SMEAppHouse.Core.Patterns.EF.ModelComposites.Abstractions;

namespace Ehrlich.PizzaSOA.Domain.Entities;

public class Pizza : GuidKeyedEntity
{
    public string PizzaId { get; set; }
    public string PizzaTypeId { get; set; }
    public string Size { get; set; }
    public decimal Price { get; set; }
}
