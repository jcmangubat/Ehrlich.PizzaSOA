using Ehrlich.PizzaSOA.Application.Models;
using Ehrlich.PizzaSOA.Application.Services.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Ehrlich.PizzaSOA.Application.Interfaces;

public interface IOrderDetailService : IServiceBase<IOrderDetailService>
{
    Task<OrderDetailModel?> Get(Guid orderDetailId);
    
    Task<IEnumerable<OrderDetailModel>?> GetOrderDetails(Guid orderId);

    Task<int> ImportAsync(IFormFile file);
}