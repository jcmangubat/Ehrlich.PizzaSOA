using Ehrlich.PizzaSOA.Application.Models;
using Ehrlich.PizzaSOA.Application.Services.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Ehrlich.PizzaSOA.Application.Interfaces;

public interface IPizzaTypeService : IServiceBase<IPizzaTypeService>
{
    Task<PizzaTypeModel?> Get(Guid pizzaTypeId);

    Task<PizzaTypeModel?> Get(string pizzaTypeCode);

    Task<IEnumerable<PizzaTypeModel>?> SearchAsync(string? typeCodePartial, string? namePartial);

    /*Task<IEnumerable<PizzaTypeModel>?> GetActivitiesByFilterAsync(Expression<Func<PizzaTypeModel, bool>> filterExpression);*/

    Task<int> ImportAsync(IFormFile file);
}
