using Ehrlich.PizzaSOA.Application.Models.Base;

namespace Ehrlich.PizzaSOA.Application.Models;

public class OrderDetailModel : GuidKeyedModel
{
    public int OrderDetailNo { get; set; }
    public int OrderNo { get; set; }
    public required string PizzaCode { get; set; }
    public required int Quantity { get; set; }

}
