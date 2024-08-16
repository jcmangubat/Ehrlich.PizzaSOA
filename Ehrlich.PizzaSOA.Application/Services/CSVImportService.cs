using CsvHelper;
using Ehrlich.PizzaSOA.Application.Interfaces;
using Ehrlich.PizzaSOA.Domain.Entities;
using Ehrlich.PizzaSOA.Domain.Interfaces.Repositories;
using Ehrlich.PizzaSOA.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace Ehrlich.PizzaSOA.Application.Services;

public class CSVImportService(ApplicationDbContext context,
    IPizzaTypeRepository pizzaTypeRepository,
    IPizzaRepository pizzaRepository,
    IOrderRepository orderRepository,
    IOrderDetailRepository orderDetailRepository) : ICSVImportService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IPizzaTypeRepository _pizzaTypeRepository = pizzaTypeRepository;
    private readonly IPizzaRepository _pizzaRepository = pizzaRepository;
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IOrderDetailRepository _orderDetailRepository = orderDetailRepository;

    public async Task ImportOrdersAsync(IFormFile file)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var orders = csv.GetRecords<Order>().ToList();
        _context.Orders.AddRange(orders);
        await _context.SaveChangesAsync();
    }

    public async Task ImportOrderDetailsAsync(IFormFile file)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var orderDetails = csv.GetRecords<OrderDetail>().ToList();
        _context.OrderDetails.AddRange(orderDetails);
        await _context.SaveChangesAsync();
    }

    public async Task ImportPizzaTypesAsync(IFormFile file)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var pizzaTypes = csv.GetRecords<PizzaType>().ToList();
        _context.PizzaTypes.AddRange(pizzaTypes);
        await _context.SaveChangesAsync();
    }

    public async Task ImportPizzasAsync(IFormFile file)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var pizzas = csv.GetRecords<Pizza>().ToList();
        _context.Pizzas.AddRange(pizzas);
        await _context.SaveChangesAsync();
    }
}
