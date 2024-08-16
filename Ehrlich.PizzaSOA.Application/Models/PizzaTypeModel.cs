using Ehrlich.PizzaSOA.Application.Models.Base;

namespace Ehrlich.PizzaSOA.Application.Models;

public class PizzaTypeModel : GuidKeyedModel
{
    public required string PizzaTypeCode { get; set; }
    public required string Name { get; set; }
    public required string Category { get; set; }
    public required string Ingredients { get; set; }

    public virtual IEnumerable<PizzaModel>? Pizzas { get; set; } = [];
}
