using Ehrlich.PizzaSOA.Application.Models;
using Ehrlich.PizzaSOA.Application.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using static Ehrlich.PizzaSOA.Domain.Constants.Rules;

namespace Ehrlich.PizzaSOA.Application.Interfaces;

public interface IPizzaService : IServiceBase<IPizzaService>
{
    Task<PizzaModel?> Get(Guid pizzaId);
    Task<PizzaModel?> Get(string pizzaCode);
    Task<IEnumerable<PizzaModel>?> SearchAsync(PizzaSizesEnum? pizzaSize, decimal? priceLow, decimal? priceHigh);

    Task<int> ImportAsync(IFormFile file);
}
