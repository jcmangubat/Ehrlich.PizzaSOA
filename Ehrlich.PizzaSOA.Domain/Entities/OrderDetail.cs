using SMEAppHouse.Core.Patterns.EF.ModelComposites.Abstractions;

namespace Ehrlich.PizzaSOA.Domain.Entities;

public class OrderDetail : GuidKeyedEntity
{
    public required int Quantity { get; set; }

    public Guid PizzaId { get; set; }
    public virtual required Pizza Pizza { get; set; }

    public Guid OrderId { get; set; }
    public virtual required Order Order { get; set; }
}
