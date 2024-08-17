using Ehrlich.PizzaSOA.Application.Models;
using Ehrlich.PizzaSOA.IntegrationTest.Helpers;
using Ehrlich.PizzaSOA.WebAPI;
using Ehrlich.PizzaSOA.WebAPI.Models;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace Ehrlich.PizzaSOA.IntegrationTest.OrderTests;

public class OrderControllerTests(CustomWebApplicationFactory<Startup> factory)
    : IClassFixture<CustomWebApplicationFactory<Startup>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GetOrderById_WithValidId_ReturnsOkWithOrder()
    {
        // Arrange
        var sampleOrder = await GetOneOrderAsync();
        sampleOrder.Should().NotBeNull();

        var orderId = sampleOrder?.Id;
        var authToken = await AuthAndGetTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken.Token);

        // Act
        var response = await _client.GetAsync($"/api/order/id/{orderId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<OrderModel>(content);

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetOrderById_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var invalidOrderId = Guid.NewGuid();
        var authToken = await AuthAndGetTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken.Token);

        // Act
        var response = await _client.GetAsync($"/api/order/id/{invalidOrderId}");

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    internal async Task<LoginResponse> AuthAndGetTokenAsync()
    {
        var loginModel = new
        {
            Username = "test",
            Password = "password"
        };

        var requestContent = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/auth/login")
        {
            Content = requestContent
        };

        var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<LoginResponse>(content);
    }

    internal async Task<OrderModel?> GetOneOrderAsync()
    {
        var startDate = DateTime.Parse("01/01/2015");
        var endDate = DateTime.Parse("05/03/2024");
        var response = await _client.GetAsync($"/api/order/search?orderDateLow={startDate}&orderDateHigh={endDate}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<List<OrderModel>>(content);

        if (result == null)
            return null;

        var random = new Random();
        return result[random.Next(result.Count)];
    }
}
