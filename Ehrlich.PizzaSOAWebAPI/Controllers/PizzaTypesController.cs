using Ehrlich.PizzaSOA.Application.Interfaces;
using Ehrlich.PizzaSOA.WebAPI.Controllers.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Ehrlich.PizzaSOA.WebAPI.Controllers;

/// <summary>
/// This controller is responsible for managing pizza types. 
/// While this does not expose complete endpoints that allow clients to perform CRUD operations on 
/// pizza types, it focuses on reading and importing of the CSV file.
/// </summary>
/// <param name="logger"></param>
/// <param name="pizzaTypeService"></param>
[ApiController]
[Route("api/[controller]")]
public class PizzaTypesController(ILogger<PizzaTypesController> logger, IPizzaTypeService pizzaTypeService)
    : APIControllerBase<PizzaTypesController>(logger)
{
    private readonly IPizzaTypeService _pizzaTypeService = pizzaTypeService
        ?? throw new ArgumentNullException(nameof(pizzaTypeService));

    /// <summary>
    /// Retrieves a pizza type by its unique identifier.
    /// </summary>
    /// <param name="pizzaTypeId">The unique identifier of the pizza type.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation. 
    /// Returns a 200 OK response with the pizza type data if found; otherwise, a 404 Not Found response.</returns>
    [HttpGet("{pizzaTypeId:guid}")]
    public async Task<IActionResult> GetPizzaTypeById(Guid pizzaTypeId)
    {
        var pizzaType = await _pizzaTypeService.Get(pizzaTypeId);
        if (pizzaType == null)
            return NotFound();

        return Ok(pizzaType);
    }

    /// <summary>
    /// Retrieves a pizza type by its code.
    /// </summary>
    /// <param name="pizzaTypeCode">The code of the pizza type.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation. 
    /// Returns a 200 OK response with the pizza type data if found; otherwise, a 404 Not Found response.</returns>
    [HttpGet("bycode/{pizzaTypeCode}")]
    public async Task<IActionResult> GetPizzaTypeByCode(string pizzaTypeCode)
    {
        var pizzaType = await _pizzaTypeService.Get(pizzaTypeCode);
        if (pizzaType == null)
            return NotFound();

        return Ok(pizzaType);
    }

    /// <summary>
    /// Retrieves a list of pizza types filtered by a partial code.
    /// </summary>
    /// <param name="pizzaTypeCodePartial">The partial code used to filter the pizza types.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation. 
    /// Returns a 200 OK response with a list of pizza types if any are found; otherwise, a 404 Not Found response.</returns>
    [HttpGet("list")]
    public async Task<IActionResult> GetPizzaTypeList([FromQuery] string pizzaTypeCodePartial)
    {
        var pizzaTypes = await _pizzaTypeService.GetListAsync(pizzaTypeCodePartial);
        if (pizzaTypes == null || !pizzaTypes.Any())
            return NotFound();
        return Ok(pizzaTypes);
    }

    /// <summary>
    /// Imports pizza types from an uploaded file.
    /// </summary>
    /// <param name="file">The file containing pizza types data.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the import operation.
    /// Returns a 200 OK response if the import is successful; otherwise, a 400 Bad Request response if the file is null or empty.</returns>
    [HttpPost("import")]
    public async Task<IActionResult> ImportPizzaTypes(IFormFile file)
    {
        // Previously encountered an issue with this endpoing documenting with Swashbuckle while param is
        // decorated with [FromForm] attribute. This link below provided the reason and the way to get it fixed:
        // https://github.com/domaindrivendev/Swashbuckle.AspNetCore?tab=readme-ov-file#handle-forms-and-file-uploads

        if (file == null || file.Length == 0)
            return BadRequest("File is not selected or empty.");

        await _pizzaTypeService.ImportAsync(file);
        return Ok("Import successful.");
    }
}
