namespace Ehrlich.PizzaSOA.Application.Models;

public class PizzaModel
{
    public required string PizzaId { get; set; }
    public required string PizzaTypeId { get; set; }
    public required string Size { get; set; }
    public required decimal Price { get; set; }
}
