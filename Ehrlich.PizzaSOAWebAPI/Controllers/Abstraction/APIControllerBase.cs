using Microsoft.AspNetCore.Mvc;

namespace Ehrlich.PizzaSOA.WebAPI.Controllers.Abstraction;

public class APIControllerBase<TController>(ILogger<TController> logger) : ControllerBase
    where TController : ControllerBase
{
    public ILogger<TController> Logger { get; } = logger ?? throw new ArgumentNullException(nameof(logger));
}
