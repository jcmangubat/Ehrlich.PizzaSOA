using Ehrlich.PizzaSOA.Application.Interfaces;
using Ehrlich.PizzaSOA.WebAPI.Controllers.Abstraction;
using Microsoft.AspNetCore.Mvc;
using SMEAppHouse.Core.CodeKits.Helpers;

namespace Ehrlich.PizzaSOA.WebAPI.Controllers;

/// <summary>
/// Controller for managing orders. Provides endpoints for retrieving, importing, and searching orders.
/// </summary>
/// <param name="logger"></param>
/// <param name="orderService"></param>
[ApiController]
[Route("api/[controller]")]
public class OrderController(ILogger<OrderController> logger, IOrderService orderService)
    : APIControllerBase<OrderController>(logger)
{
    private readonly IOrderService _orderService = orderService
        ?? throw new ArgumentNullException(nameof(orderService));

    /// <summary>
    /// Retrieves a order by its unique identifier.
    /// </summary>
    /// <param name="orderId">The unique identifier of the order.</param>
    /// <returns>A order model if found, otherwise NotFound.</returns>
    [HttpGet("id/{orderId}")]
    public async Task<IActionResult> GetOrderById(Guid orderId)
    {
        try
        {
            var order = await _orderService.Get(orderId);
            if (order == null)
                return NotFound();

            return Ok(order);
        }
        catch (ApplicationException ex)
        {
            base.Logger.LogError(ex, ex.Message); // Log detailed error
            return BadRequest("The request could not be processed. Please check your input and try again.");
        }
        catch (Exception ex)
        {
            base.Logger.LogError(ex, "An error occurred while searching for orders.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }


    /// <summary>
    /// Imports order data from a CSV file. Only new orders that do not already exist in the system will be imported.
    /// </summary>
    /// <param name="file">The CSV file containing the order data.</param>
    /// <returns>The number of new orders imported.</returns>
    [HttpPost("import")]
    public async Task<IActionResult> ImportOrders(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        try
        {
            var count = await _orderService.ImportAsync(file);
            return Ok(new { ImportedCount = count });
        }
        catch (ApplicationException ex)
        {
            base.Logger.LogError(ex, ex.Message); // Log detailed error
            return BadRequest("The request could not be processed. Please check your input and try again.");
        }
        catch (Exception ex)
        {
            base.Logger.LogError(ex, "An error occurred while searching for orders.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Searches for orders based on order number, date range, and time range.
    /// </summary>
    /// <param name="orderNo">The order number to   search for.</param>
    /// <param name="orderDateLow">The start of the order date range.</param>
    /// <param name="orderDateHigh">The end of the order date range.</param>
    /// <param name="orderTimeLow">start of the order time range.</param>
    /// <param name="orderTimeHigh">The end of the order time range.</param>
    /// <returns>Retursn list of orders that match the search criteria.</returns>
    [HttpGet("search")]
    public async Task<IActionResult> SearchOrders([FromQuery] int? orderNo,
                                                  [FromQuery] DateTime? orderDateLow, [FromQuery] DateTime? orderDateHigh,
                                                  [FromQuery] TimeSpan? orderTimeLow, [FromQuery] TimeSpan? orderTimeHigh)
    {
        try
        {
            var orders = await _orderService.SearchAsync(orderNo, orderDateLow, orderDateHigh, orderTimeLow, orderTimeHigh);
            return Ok(orders);
        }
        catch (ArgumentException ex)
        {
            base.Logger.LogWarning(ex, ex.GetExceptionMessages());
            return BadRequest(ex.Message);
        }
        catch (ApplicationException ex)
        {
            base.Logger.LogError(ex, "An error occurred while searching for orders.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
        catch (Exception ex)
        {
            base.Logger.LogError(ex, "An unexpected error occurred.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
        }
    }
}
