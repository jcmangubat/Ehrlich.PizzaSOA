using SMEAppHouse.Core.Patterns.EF.ModelComposites.Abstractions;
using static Ehrlich.PizzaSOA.Domain.Constants.Rules;

namespace Ehrlich.PizzaSOA.Domain.Entities;

public class PizzaType : GuidKeyedEntity
{
    public required string PizzaTypeCode { get; set; }
    public required string Name { get; set; }
    public PizzaTypeCategoriesEnum Category { get; set; }
    public required string Ingredients { get; set; }

    public virtual List<Pizza>? Pizzas { get; set; }
}
