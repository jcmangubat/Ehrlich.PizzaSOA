using Ehrlich.PizzaSOA.IntegrationTest.Helpers;
using Ehrlich.PizzaSOA.WebAPI;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Ehrlich.PizzaSOA.IntegrationTest.OrderTests;

/// <summary>
/// Reason for using CustomWebApplicationFactory is to create a test server that mimics the production environment, 
/// allowing the test of the API endpoints in a controlled, isolated setting. 
/// Use of the default Test Server Factory, WebApplicationFactory<Startup> default to AspNetCore.MVC.Testing assembly 
/// recently throws an issue where the connection string is needed present in the test project wich is not ideal.
/// </summary>
/// <param name="factory"></param>
public class OrderControllerImportTests(CustomWebApplicationFactory<Startup> factory)
    : IClassFixture<CustomWebApplicationFactory<Startup>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task ImportOrders_WithValidFile_ReturnsOkWithImportCount()
    {
        // Arrange
        var fileContent = "order_id,date,time\n1,01/01/2015,11:38:36\n2,01/01/2015,11:57:40";
        var content = new MultipartFormDataContent();
        var fileStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(fileContent));
        var fileContentStream = new StreamContent(fileStream);
        fileContentStream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
        content.Add(fileContentStream, "file", "orders.csv");

        // Act
        var response = await _client.PostAsync("/api/order/import", content);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<dynamic>(responseContent);

        ((int)result.importedCount).Should().Be(2);

        /*result.Should().NotBeNull();
        result.Should().HaveProperty("ImportedCount");*/
    }

    [Fact]
    public async Task ImportOrders_WithNoFile_ReturnsBadRequest()
    {
        // Arrange
        var content = new MultipartFormDataContent(); // Empty content

        // Act
        var response = await _client.PostAsync("/api/order/import", content);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }
}