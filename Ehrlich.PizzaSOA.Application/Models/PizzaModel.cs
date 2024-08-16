using Ehrlich.PizzaSOA.Application.Models.Base;

namespace Ehrlich.PizzaSOA.Application.Models;

public class PizzaModel : GuidKeyedModel
{
    public required string PizzaCode { get; set; }
    public required string PizzaTypeCode { get; set; }
    public required string Size { get; set; }
    public required decimal Price { get; set; }

    public virtual IEnumerable<OrderDetailModel>? OrderDetails { get; set; } = [];
}
