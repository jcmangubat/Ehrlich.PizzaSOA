using Ehrlich.PizzaSOA.Application.Models;
using Ehrlich.PizzaSOA.Application.Services.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Ehrlich.PizzaSOA.Application.Interfaces;

public interface IOrderService : IServiceBase<IOrderService>
{
    Task<OrderModel?> Get(Guid orderId);
    
    Task<IEnumerable<OrderModel>?> SearchAsync(int? orderNo, DateTime? orderDateLow, DateTime? orderDateHigh, TimeSpan? orderTimeLow, TimeSpan? orderTimeHigh);

    Task<int> ImportAsync(IFormFile file);
}