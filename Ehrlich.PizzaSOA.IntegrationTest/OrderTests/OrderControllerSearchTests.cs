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
        var orderNo = 1; // Adjust criteria as needed
        var startDate = DateTime.UtcNow.AddDays(-7).ToString("yyyy-MM-dd");
        var endDate = DateTime.UtcNow.ToString("yyyy-MM-dd");

        // Act
        var response = await _client.GetAsync($"/api/order/search?orderNo={orderNo}&orderDateLow={startDate}&orderDateHigh={endDate}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<dynamic>(content);

        result.Should().NotBeNull();
        result.Should().BeOfType<List<dynamic>>(); // Replace with actual type
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