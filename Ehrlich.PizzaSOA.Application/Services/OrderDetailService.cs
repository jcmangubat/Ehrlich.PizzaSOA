using AutoMapper;
using CsvHelper;
using Ehrlich.PizzaSOA.Application.Interfaces;
using Ehrlich.PizzaSOA.Application.Mappings.CSVFieldMappings;
using Ehrlich.PizzaSOA.Application.Models;
using Ehrlich.PizzaSOA.Application.Services.Abstractions;
using Ehrlich.PizzaSOA.Domain.Entities;
using Ehrlich.PizzaSOA.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMEAppHouse.Core.CodeKits.Helpers;
using System.Globalization;

namespace Ehrlich.PizzaSOA.Application.Services;

public class OrderDetailService(IMapper mapper,
                        ILogger<IOrderDetailService> logger,
                        IOrderDetailRepository orderDetailRepository,
                        IOrderRepository orderRepository,
                        IPizzaRepository pizzaRepository) :
    ServiceBase<IOrderDetailService>(mapper, logger), IOrderDetailService
{
    private readonly IOrderDetailRepository _orderDetailRepository = orderDetailRepository ??
                                        throw new ArgumentNullException(nameof(orderDetailRepository));

    private readonly IOrderRepository _orderRepository = orderRepository ??
                                        throw new ArgumentNullException(nameof(orderRepository));

    private readonly IPizzaRepository _pizzaRepository = pizzaRepository ??
                                        throw new ArgumentNullException(nameof(pizzaRepository));

    public async Task<OrderDetailModel?> Get(Guid orderDetailId)
    {
        try
        {
            var order = await _orderDetailRepository.GetSingleAsync(p => p.Id == orderDetailId);
            return base.Mapper.Map<OrderDetailModel>(order);
        }
        catch (Exception ex)
        {
            base.Logger.LogError(ex, ex.GetExceptionMessages());
            throw new ApplicationException("An error occurred while processing your request.");
        }
    }

    public async Task<IEnumerable<OrderDetailModel>?> GetOrderDetails(Guid orderId)
    {
        try
        {
            var orderDetails = await _orderDetailRepository.GetListAsync(p => p.Id == orderId);
            return base.Mapper.Map<List<OrderDetailModel>>(orderDetails);
        }
        catch (Exception ex)
        {
            base.Logger.LogError(ex, ex.GetExceptionMessages());
            throw new ApplicationException("An error occurred while processing your request.");
        }
    }

    public async Task<int> ImportAsync(IFormFile file)
    {
        try
        {
            using var reader = new StreamReader(file.OpenReadStream());

            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<OrderDetailModelMap>();
            var orderDetailModels = csv.GetRecords<OrderDetailModel>().ToList();

            var orderNos = orderDetailModels.Select(p => p.OrderNo).Distinct().ToList();
            var ordersDic = await _orderRepository.DbContext.Set<Order>()
                                        .Select(o => new { OrderId = o.Id, o.OrderNo })
                                        .ToDictionaryAsync(p => p.OrderNo, p => p.OrderId);

            var pizzasDic = await _pizzaRepository.DbContext.Set<Pizza>()
                                        .Select(o => new { PizzaId = o.Id, o.PizzaCode })
                                        .ToDictionaryAsync(p => p.PizzaCode, p => p.PizzaId);

            var orderDetails = orderDetailModels.Select(p => new OrderDetail
            {
                Id = Guid.NewGuid(),
                OrderDetailNo = p.OrderDetailNo,
                Quantity = p.Quantity,
                OrderId = ordersDic[p.OrderNo],
                PizzaId = pizzasDic[p.PizzaCode]
            });

            // Fetch existing order details from the repository
            var existingOrderDetailNos = (await _orderDetailRepository.GetListAsync())
                                          .Select(pt => pt.OrderDetailNo)
                                          .ToHashSet();

            //Filter out existing PizzaTypes
            var newOrderDetails = orderDetails
                .Where(p => !existingOrderDetailNos.Contains(p.OrderDetailNo))
                .ToList();

            if (newOrderDetails == null || newOrderDetails.Count == 0)
                return 0;

            await _orderDetailRepository.AddAsync(newOrderDetails);
            await _orderDetailRepository.CommitAsync();

            return newOrderDetails.Count;
        }
        catch (Exception ex)
        {
            base.Logger.LogError(ex, ex.GetExceptionMessages());
            throw new ApplicationException("An error occurred while processing your request.");
        }
    }
}