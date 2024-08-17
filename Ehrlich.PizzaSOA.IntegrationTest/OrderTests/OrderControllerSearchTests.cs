using Ehrlich.PizzaSOA.Application.Models;
using Ehrlich.PizzaSOA.IntegrationTest.Helpers;
using Ehrlich.PizzaSOA.WebAPI;
using FluentAssertions;
using Newtonsoft.Json;

namespace Ehrlich.PizzaSOA.IntegrationTest.OrderTests;

public class OrderControllerSearchTests(CustomWebApplicationFactory<Startup> factory) 
    : IClassFixture<CustomWebApplicationFactory<Startup>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task SearchOrders_WithValidCriteria_ReturnsOkWithOrders()
    {
        // Arrange
        var orderNo = 20; // Adjust criteria as needed
        /*var startDate = DateTime.UtcNow.AddDays(-7).ToString("yyyy-MM-dd");
        var endDate = DateTime.UtcNow.ToString("yyyy-MM-dd");*/

        var startDate = DateTime.Parse("01/01/2015");
        var endDate = DateTime.Parse("01/01/2015");

        // Act
        var response = await _client.GetAsync($"/api/order/search?orderNo={orderNo}&orderDateLow={startDate}&orderDateHigh={endDate}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<List<OrderModel>>(content);

        result.Should().NotBeNull();
        result.Should().BeOfType<List<OrderModel>>();
        result.Should().HaveCountGreaterThan(0);
    }

    [Fact]
    public async Task SearchOrders_WithInvalidCriteria_ReturnsBadRequest()
    {
        // Act
        var response = await _client.GetAsync("/api/order/search?orderNo=invalid");

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }
}