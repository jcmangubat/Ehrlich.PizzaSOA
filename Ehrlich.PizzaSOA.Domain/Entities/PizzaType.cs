using SMEAppHouse.Core.Patterns.EF.ModelComposites.Abstractions;

namespace Ehrlich.PizzaSOA.Domain.Entities;

public class PizzaType : GuidKeyedEntity
{
    public string PizzaTypeId { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string Ingredients { get; set; }
}
