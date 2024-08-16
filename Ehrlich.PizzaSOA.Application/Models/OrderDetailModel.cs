namespace Ehrlich.PizzaSOA.Application.Models;

public class OrderDetailModel
{
    public int OrderDetailsId { get; set; }
    public int OrderId { get; set; }
    public string PizzaId { get; set; }
    public int Quantity { get; set; }
}
