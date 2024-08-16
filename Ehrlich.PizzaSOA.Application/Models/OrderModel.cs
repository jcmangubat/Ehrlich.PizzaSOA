namespace Ehrlich.PizzaSOA.Application.Models;

public class OrderModel
{
    public int OrderId { get; set; }
    public required DateTime DateOrdered { get; set; }
    public required TimeSpan TimeOrdered { get; set; }
}
