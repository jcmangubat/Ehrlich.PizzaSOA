using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Ehrlich.PizzaSOA.Application.Services.Abstractions;

public class ServiceBase<TService>(IMapper mapper, ILogger<TService> logger) : IServiceBase<TService>
    where TService : IServiceBase<TService>
{
    public IMapper Mapper { get; } = mapper ?? throw new ArgumentNullException(nameof(mapper));
    public ILogger<TService> Logger { get; } = logger ?? throw new ArgumentNullException(nameof(logger));
}
