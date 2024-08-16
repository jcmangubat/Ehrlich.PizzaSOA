using CsvHelper.Configuration;
using Ehrlich.PizzaSOA.Application.Models;

namespace Ehrlich.PizzaSOA.Application.Mappings.CSVFieldMappings;

public class PizzaTypeModelMap : ClassMap<PizzaTypeModel>
{
    public PizzaTypeModelMap()
    {
        Map(m => m.PizzaTypeCode).Name("pizza_type_id");
        Map(m => m.Name).Name("name");
        Map(m => m.Category).Name("category");
        Map(m => m.Ingredients).Name("ingredients");
    }
}
