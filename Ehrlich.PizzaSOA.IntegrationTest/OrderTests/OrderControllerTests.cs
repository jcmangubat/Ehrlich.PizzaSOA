using Ehrlich.PizzaSOA.IntegrationTest.Helpers;
using Ehrlich.PizzaSOA.WebAPI;
using FluentAssertions;
using Newtonsoft.Json;

namespace Ehrlich.PizzaSOA.IntegrationTest.OrderTests;

public class OrderControllerTests(CustomWebApplicationFactory<Startup> factory)
    : IClassFixture<CustomWebApplicationFactory<Startup>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GetOrderById_WithValidId_ReturnsOkWithOrder()
    {
        // Arrange
        var orderId = Guid.NewGuid(); // Replace with an existing ID if needed

        // Act
        var response = await _client.GetAsync($"/api/order/id/{orderId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<dynamic>(content);

        result.Should().NotBeNull();
        result.Should().HaveProperty("orderId");
        // Add more assertions based on the structure of your order object
    }

    [Fact]
    public async Task GetOrderById_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var invalidOrderId = Guid.NewGuid();

        // Act
        var response = await _client.GetAsync($"/api/order/id/{invalidOrderId}");

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
}
