using Ehrlich.PizzaSOA.WebAPI;
using Ehrlich.PizzaSOA.WebAPI.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;

namespace Ehrlich.PizzaSOA.IntegrationTest.AuthTests;

public class AuthControllerTests(WebApplicationFactory<Startup> factory) : IClassFixture<WebApplicationFactory<Startup>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Login_WithValidCredentials_ReturnsOkWithToken()
    {
        // Arrange
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

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<LoginResponse>(content);

        result.Should().NotBeNull();
        result.Token.Should().NotBeNullOrEmpty();
        result.Token.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
    {
        // Arrange
        var loginModel = new
        {
            Username = "test",
            Password = "wrongpassword"
        };

        var requestContent = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/auth/login")
        {
            Content = requestContent
        };

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
    }
}
