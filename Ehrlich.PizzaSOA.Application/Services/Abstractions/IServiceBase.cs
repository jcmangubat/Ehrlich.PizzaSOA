using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Ehrlich.PizzaSOA.Application.Services.Abstractions;

public interface IServiceBase<TService>
{
    IMapper Mapper { get; }
    ILogger<TService> Logger { get; }
}
