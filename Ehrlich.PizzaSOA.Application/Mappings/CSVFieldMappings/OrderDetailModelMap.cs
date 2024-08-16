using CsvHelper.Configuration;
using Ehrlich.PizzaSOA.Application.Models;

namespace Ehrlich.PizzaSOA.Application.Mappings.CSVFieldMappings;

public class OrderDetailModelMap : ClassMap<OrderDetailModel>
{
    public OrderDetailModelMap()
    {
        Map(m => m.OrderDetailNo).Name("order_details_id");
        Map(m => m.OrderNo).Name("order_id");
        Map(m => m.PizzaCode).Name("pizza_id");
        Map(m => m.Quantity).Name("quantity");
    }
}