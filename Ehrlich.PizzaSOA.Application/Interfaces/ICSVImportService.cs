using Microsoft.AspNetCore.Http;

namespace Ehrlich.PizzaSOA.Application.Interfaces;

public interface ICSVImportService
{
    Task ImportOrdersAsync(IFormFile file);

    Task ImportOrderDetailsAsync(IFormFile file);

    Task ImportPizzaTypesAsync(IFormFile file);

    Task ImportPizzasAsync(IFormFile file);
}
