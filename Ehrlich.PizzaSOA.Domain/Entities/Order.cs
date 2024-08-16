using SMEAppHouse.Core.Patterns.EF.ModelComposites.Abstractions;

namespace Ehrlich.PizzaSOA.Domain.Entities;

public class Order : GuidKeyedEntity
{
    public int OrderId { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
}
