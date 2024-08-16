using CsvHelper.Configuration;
using Ehrlich.PizzaSOA.Application.Models;

namespace Ehrlich.PizzaSOA.Application.Mappings.CSVFieldMappings;

public class OrderModelMap : ClassMap<OrderModel>
{
    public OrderModelMap()
    {
        Map(m => m.OrderNo).Name("order_id");
        Map(m => m.DateOrdered).Name("date");
        Map(m => m.TimeOrdered).Name("time");
    }
}
