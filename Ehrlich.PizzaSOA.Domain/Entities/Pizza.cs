using SMEAppHouse.Core.Patterns.EF.ModelComposites.Abstractions;
using static Ehrlich.PizzaSOA.Domain.Constants.Rules;

namespace Ehrlich.PizzaSOA.Domain.Entities;

public class Pizza : GuidKeyedEntity
{
    public required string PizzaCode { get; set; }
    public required string PizzaTypeCode { get; set; }
    public required PizzaSizesEnum Size { get; set; }
    public decimal Price { get; set; }


    public required Guid PizzaTypeId { get; set; }
    public virtual PizzaType? PizzaType { get; set; }

    public virtual List<OrderDetail>? OrderDetails { get; set; }
}
