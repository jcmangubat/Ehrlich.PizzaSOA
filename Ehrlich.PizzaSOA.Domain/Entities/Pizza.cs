using SMEAppHouse.Core.Patterns.EF.ModelComposites.Abstractions;
using static Ehrlich.PizzaSOA.Domain.Constants.Rules;

namespace Ehrlich.PizzaSOA.Domain.Entities;

public class Pizza : GuidKeyedEntity
{
    public required string PizzaItemCode { get; set; }    
    public required PizzaSizesEnum Size { get; set; }
    public decimal Price { get; set; }


    public Guid PizzaTypeId { get; set; }
    public virtual required PizzaType PizzaType { get; set; }

    public virtual List<OrderDetail>? OrderDetails { get; set; }
}
