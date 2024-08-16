using CsvHelper.Configuration.Attributes;
using Ehrlich.PizzaSOA.Application.Models.Base;

namespace Ehrlich.PizzaSOA.Application.Models;

public class OrderModel: GuidKeyedModel
{
    public int OrderNo { get; set; }

    [Format("dd/MM/yyyy")]
    public required DateTime DateOrdered { get; set; }
    public required TimeSpan TimeOrdered { get; set; }

    public virtual IEnumerable<OrderDetailModel>? OrderDetails { get; set; } = [];
}
