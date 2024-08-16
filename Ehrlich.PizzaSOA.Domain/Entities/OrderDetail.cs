using SMEAppHouse.Core.Patterns.EF.ModelComposites.Abstractions;

namespace Ehrlich.PizzaSOA.Domain.Entities;

public class OrderDetail : GuidKeyedEntity
{
    public int OrderDetailsId { get; set; }
    public int OrderId { get; set; }
    public string PizzaId { get; set; }
    public int Quantity { get; set; }
}
