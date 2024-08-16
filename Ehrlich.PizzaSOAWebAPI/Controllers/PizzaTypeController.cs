using Ehrlich.PizzaSOA.Application.Interfaces;
using Ehrlich.PizzaSOA.WebAPI.Controllers.Abstraction;
using Microsoft.AspNetCore.Mvc;
using SMEAppHouse.Core.CodeKits.Helpers;

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
public class PizzaTypeController(ILogger<PizzaTypeController> logger, IPizzaTypeService pizzaTypeService)
    : APIControllerBase<PizzaTypeController>(logger)
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
        try
        {
            var pizzaType = await _pizzaTypeService.Get(pizzaTypeId);
            if (pizzaType == null)
                return NotFound();

            return Ok(pizzaType);
        }
        catch (ApplicationException ex)
        {
            base.Logger.LogError(ex, ex.Message); // Log detailed error
            return BadRequest("The request could not be processed. Please check your input and try again."); 
        }
        catch (Exception ex)
        {
            base.Logger.LogError(ex, "An error occurred while searching for pizzas.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
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
        try
        {
            var pizzaType = await _pizzaTypeService.Get(pizzaTypeCode);
            if (pizzaType == null)
                return NotFound();

            return Ok(pizzaType);
        }
        catch (ApplicationException ex)
        {
            base.Logger.LogError(ex, ex.Message); // Log detailed error
            return BadRequest("The request could not be processed. Please check your input and try again."); 
        }
        catch (Exception ex)
        {
            base.Logger.LogError(ex, "An error occurred while searching for pizzas.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Retrieves a list of pizza types filtered by a partial code.
    /// </summary>
    /// <param name="pizzaTypeCodePartial">The partial code used to filter the pizza types.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation. 
    /// Returns a 200 OK response with a list of pizza types if any are found; otherwise, a 404 Not Found response.</returns>
    [HttpGet("search")]
    public async Task<IActionResult> SearchPizzaTypes([FromQuery] string? typeCodePartial, [FromQuery] string? namePartial)
    {
        try
        {
            var pizzaTypes = await _pizzaTypeService.SearchAsync(typeCodePartial, namePartial);
            if (pizzaTypes == null || !pizzaTypes.Any())
                return NotFound();
            return Ok(pizzaTypes);
        }
        catch (ArgumentException ex)
        {
            base.Logger.LogWarning(ex, ex.GetExceptionMessages());
            return BadRequest(ex.Message);
        }
        catch (ApplicationException ex)
        {
            base.Logger.LogError(ex, ex.Message); // Log detailed error
            return BadRequest("The request could not be processed. Please check your input and try again."); 
        }
        catch (Exception ex)
        {
            base.Logger.LogError(ex, "An error occurred while searching for pizzas.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
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

        try
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is not selected or empty.");

            var count = await _pizzaTypeService.ImportAsync(file);
            return Ok(new { ImportedCount = count });
        }
        catch (ApplicationException ex)
        {
            base.Logger.LogError(ex, ex.Message); // Log detailed error
            return BadRequest("The request could not be processed. Please check your input and try again."); 
        }
        catch (Exception ex)
        {
            base.Logger.LogError(ex, "An error occurred while searching for pizzas.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }
}
