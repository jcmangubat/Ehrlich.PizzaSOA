using Ehrlich.PizzaSOA.Application.Interfaces;
using Ehrlich.PizzaSOA.WebAPI.Controllers.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Ehrlich.PizzaSOA.WebAPI.Controllers;

/// <summary>
/// Controller for managing order details. Provides endpoints for retrieving, importing, and searching order details.
/// </summary>
/// <param name="logger"></param>
/// <param name="orderDetailService"></param>
[ApiController]
[Route("api/[controller]")]
public class OrderDetailController(ILogger<OrderDetailController> logger, IOrderDetailService orderDetailService)
    : APIControllerBase<OrderDetailController>(logger)
{
    private readonly IOrderDetailService _orderDetailService = orderDetailService
        ?? throw new ArgumentNullException(nameof(orderDetailService));


    /// <summary>
    /// Retrieves a specific order detail by its ID.
    /// </summary>
    /// <param name="orderDetailId">The ID of the order detail to retrieve.</param>
    /// <returns>The order detail with the specified ID.</returns>
    [HttpGet("{orderDetailId:guid}")]
    public async Task<IActionResult> GetOrderDetail(Guid orderDetailId)
    {
        try
        {
            var orderDetail = await _orderDetailService.Get(orderDetailId);
            if (orderDetail == null)
            {
                base.Logger.LogWarning($"OrderDetail with ID {orderDetailId} not found.");
                return NotFound();
            }

            return Ok(orderDetail);
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
    /// Retrieves all order details for a specific order.
    /// </summary>
    /// <param name="orderId">The ID of the order whose details are to be retrieved.</param>
    /// <returns>A list of order details for the specified order.</returns>
    [HttpGet("order/{orderId:guid}")]
    public async Task<IActionResult> GetOrderDetails(Guid orderId)
    {
        try
        {
            var orderDetails = await _orderDetailService.GetOrderDetails(orderId);
            if (orderDetails == null || !orderDetails.Any())
            {
                base.Logger.LogWarning($"No order details found for order ID {orderId}.");
                return NotFound();
            }

            return Ok(orderDetails);
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
    /// Imports order details from a CSV file.
    /// </summary>
    /// <param name="file">The CSV file containing order details.</param>
    /// <returns>The number of order details successfully imported.</returns>
    [HttpPost("import")]
    public async Task<IActionResult> ImportOrderDetails(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            base.Logger.LogWarning("Invalid file uploaded.");
            return BadRequest("Invalid file uploaded.");
        }

        try
        {
            var result = await _orderDetailService.ImportAsync(file);
            return Ok(new { ImportedRecords = result });
        }
        catch (Exception ex)
        {
            base.Logger.LogError(ex, "An error occurred while importing order details.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }
}
