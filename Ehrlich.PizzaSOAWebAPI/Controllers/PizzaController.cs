using Ehrlich.PizzaSOA.Application.Interfaces;
using Ehrlich.PizzaSOA.Domain.Constants;
using Ehrlich.PizzaSOA.WebAPI.Controllers.Abstraction;
using Microsoft.AspNetCore.Mvc;
using SMEAppHouse.Core.CodeKits.Helpers;

namespace Ehrlich.PizzaSOA.WebAPI.Controllers;

/// <summary>
/// Controller for handling pizza-related operations.
/// </summary>
/// <param name="logger"></param>
/// <param name="pizzaService"></param>
[ApiController]
[Route("api/[controller]")]
public class PizzaController(ILogger<PizzaController> logger, IPizzaService pizzaService)
    : APIControllerBase<PizzaController>(logger)
{
    private readonly IPizzaService _pizzaService = pizzaService
        ?? throw new ArgumentNullException(nameof(pizzaService));

    /// <summary>
    /// Retrieves a pizza by its unique identifier.
    /// </summary>
    /// <param name="pizzaId">The unique identifier of the pizza.</param>
    /// <returns>A pizza model if found, otherwise NotFound.</returns>
    [HttpGet("id/{pizzaId}")]
    public async Task<IActionResult> GetPizzaById(Guid pizzaId)
    {
        try
        {
            var pizza = await _pizzaService.Get(pizzaId);
            if (pizza == null)
            {
                return NotFound();
            }
            return Ok(pizza);
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
    /// Retrieves a pizza by its unique code.
    /// </summary>
    /// <param name="pizzaCode">The unique code of the pizza.</param>
    /// <returns>A pizza model if found, otherwise NotFound.</returns>
    [HttpGet("code/{pizzaCode}")]
    public async Task<IActionResult> GetPizzaByCode(string pizzaCode)
    {
        try
        {
            var pizza = await _pizzaService.Get(pizzaCode);
            if (pizza == null)
            {
                return NotFound();
            }
            return Ok(pizza);
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
    /// Imports pizzas from a CSV file.
    /// </summary>
    /// <param name="file">The CSV file containing pizza data.</param>
    /// <returns>Number of pizzas successfully imported.</returns>
    [HttpPost("import")]
    public async Task<IActionResult> ImportPizzas(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");
        
        try
        {
            var count = await _pizzaService.ImportAsync(file);
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

    /// <summary>
    /// Searches for pizzas based on specified criteria.
    /// </summary>
    /// <param name="pizzaSize">Optional pizza size to filter by.</param>
    /// <param name="priceLow">Optional minimum price to filter by.</param>
    /// <param name="priceHigh">Optional maximum price to filter by.</param>
    /// <returns>A list of pizzas matching the criteria.</returns>
    [HttpGet("search")]
    public async Task<IActionResult> SearchPizzas([FromQuery] Rules.PizzaSizesEnum? pizzaSize, [FromQuery] decimal? priceLow, [FromQuery] decimal? priceHigh)
    {
        try
        {
            var pizzas = await _pizzaService.SearchAsync(pizzaSize, priceLow, priceHigh);
            return Ok(pizzas);
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
}
