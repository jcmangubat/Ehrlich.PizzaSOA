using CsvHelper.Configuration;
using Ehrlich.PizzaSOA.Application.Models;

namespace Ehrlich.PizzaSOA.Application.Mappings.CSVFieldMappings;

//Pizza: pizza_id	pizza_type_id	size	price
public class PizzaModelMap : ClassMap<PizzaModel>
{
    public PizzaModelMap()
    {
        Map(m => m.PizzaCode).Name("pizza_id");
        Map(m => m.PizzaTypeCode).Name("pizza_type_id");
        Map(m => m.Size).Name("size");
        Map(m => m.Price).Name("price");
    }
}