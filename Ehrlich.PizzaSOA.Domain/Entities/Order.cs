using SMEAppHouse.Core.Patterns.EF.ModelComposites.Abstractions;

namespace Ehrlich.PizzaSOA.Domain.Entities;

public class Order : GuidKeyedEntity
{
    public int OrderNo { get; set; }
    public required DateTime DateOrdered { get; set; }
    public required TimeSpan TimeOrdered { get; set; }

    public virtual List<OrderDetail>? OrderDetails { get; set; }
}
