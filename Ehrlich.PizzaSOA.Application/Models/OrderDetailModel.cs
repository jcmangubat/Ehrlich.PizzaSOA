namespace Ehrlich.PizzaSOA.Application.Models;

public class OrderDetailModel
{
    public int OrderDetailsId { get; set; }
    public int OrderId { get; set; }
    public required string PizzaId { get; set; }
    public required int Quantity { get; set; }
}
