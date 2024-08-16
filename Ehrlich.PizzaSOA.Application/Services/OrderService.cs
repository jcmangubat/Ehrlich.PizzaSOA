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
using System.Linq.Expressions;

namespace Ehrlich.PizzaSOA.Application.Services;

public class OrderService(IMapper mapper,
                        ILogger<IOrderService> logger,
                        IOrderRepository orderRepository) :
    ServiceBase<IOrderService>(mapper, logger), IOrderService
{
    private readonly IOrderRepository _orderRepository = orderRepository
                            ?? throw new ArgumentNullException(nameof(orderRepository));

    public async Task<OrderModel?> Get(Guid orderId)
    {
        try
        {
            var order = await _orderRepository.GetSingleAsync(p => p.Id == orderId, includeExpression: p => p.Include(x => x.OrderDetails));
            return base.Mapper.Map<OrderModel>(order);
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
            csv.Context.RegisterClassMap<OrderModelMap>();
            csv.Context.TypeConverterOptionsCache.GetOptions<DateTime>().Formats = ["dd/MM/yyyy"];

            var orderModels = csv.GetRecords<OrderModel>().ToList();
            var orders = base.Mapper.Map<List<Order>>(orderModels); // map to domain models

            // Fetch existing PizzaCodes from the repository
            var existingOrderNos = (await _orderRepository.GetListAsync())
                                          .Select(pt => pt.OrderNo)
                                          .ToHashSet();

            // Filter out existing PizzaTypes
            var newOrders = orders
                .Where(p => !existingOrderNos.Contains(p.OrderNo))
                .ToList();

            if (newOrders== null || newOrders.Count == 0)
                return 0;

            await _orderRepository.AddAsync(newOrders);
            await _orderRepository.CommitAsync();

            return newOrders.Count;
        }
        catch (Exception ex)
        {
            base.Logger.LogError(ex, ex.GetExceptionMessages());
            throw new ApplicationException("An error occurred while processing your request.");
        }
    }

    public async Task<IEnumerable<OrderModel>?> SearchAsync(int? orderNo, 
                                            DateTime? orderDateLow, DateTime? orderDateHigh, 
                                            TimeSpan? orderTimeLow, TimeSpan? orderTimeHigh)
    {
        try
        {
            if (orderDateLow != null && orderDateHigh != null && orderDateHigh < orderDateLow)
                throw new ArgumentException("Date range is incorrect.");

            if (orderTimeLow != null && orderTimeHigh != null && orderTimeHigh < orderTimeLow)
                throw new ArgumentException("Time range is incorrect.");

            Expression<Func<Order, bool>> predicate = p =>
                                        (orderNo == null || p.OrderNo == orderNo) &&
                                        ((orderDateLow == null && orderDateHigh == null) ||
                                        (orderDateLow != null && orderDateHigh != null && p.DateOrdered >= orderDateLow && p.DateOrdered <= orderDateHigh) ||
                                        (orderDateLow != null && orderDateHigh == null && p.DateOrdered >= orderDateLow) ||
                                        (orderDateLow == null && orderDateHigh != null && p.DateOrdered <= orderDateHigh)) &&
                                        ((orderTimeLow == null && orderTimeHigh == null) ||
                                        (orderTimeLow != null && orderTimeHigh != null && p.TimeOrdered >= orderTimeLow && p.TimeOrdered <= orderTimeHigh) ||
                                        (orderTimeLow != null && orderTimeHigh == null && p.TimeOrdered >= orderTimeLow) ||
                                        (orderTimeLow == null && orderTimeHigh != null && p.TimeOrdered <= orderTimeHigh));

            var orders = await _orderRepository.GetListAsync(predicate);

            return base.Mapper.Map<List<OrderModel>>(orders);
        }
        catch (Exception ex)
        {
            base.Logger.LogError(ex, ex.GetExceptionMessages());
            throw new ApplicationException("An error occurred while processing your request.");
        }
    }
}