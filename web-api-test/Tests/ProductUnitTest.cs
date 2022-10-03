using System.Diagnostics;
using System.Net;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;
using Xunit.Extensions.Ordering;

namespace web_api_test.Tests;

public class ProductUnitTest
{
    [Fact]
    [Order(1)]
    public async Task GetAllTest()
    {
        // Arrange
        var expectedStatusCode = HttpStatusCode.OK;
        var expectedContent = new[]
        {
            new Product(1, "Apple", 1000),
            new Product(2, "Orange", 1500),
            new Product(3, "Banana", 2000)
        };
        var stopwatch = Stopwatch.StartNew();

        // Act
        var response = await TestHelper.HttpClient.GetAsync("api/product");

        // Assert
        await TestHelper.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }

    [Fact]
    [Order(2)]
    public async Task GetByIdTest()
    {
        // Arrange
        var expectedStatusCode = HttpStatusCode.OK;
        var expectedContent = new Product(1, "Apple", 1000);
        var stopwatch = Stopwatch.StartNew();

        // Act
        var response = await TestHelper.HttpClient.GetAsync("api/product/" + expectedContent.Id);

        // Assert
        await TestHelper.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }

    [Fact]
    [Order(3)]
    public async Task SaveTest()
    {
        // Arrange
        var expectedStatusCode = HttpStatusCode.OK;
        var expectedContent = new Product(4, "Table", 3500);
        var json = JsonConvert.SerializeObject(expectedContent);
        var responseBody = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        var stopwatch = Stopwatch.StartNew();

        // Act
        var response = await TestHelper.HttpClient.PostAsync("api/product/", responseBody);

        // Assert
        await TestHelper.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }

    [Fact]
    [Order(4)]
    public async Task UpdateTest()
    {
        // Arrange
        var expectedStatusCode = HttpStatusCode.OK;
        var expectedContent = new Product(4, "Table", 3650);
        var json = JsonConvert.SerializeObject(expectedContent);
        var responseBody = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        var stopwatch = Stopwatch.StartNew();

        // Act
        var response = await TestHelper.HttpClient.PutAsync("api/product/" + expectedContent.Id, responseBody);

        // Assert
        await TestHelper.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }

    [Fact]
    [Order(5)]
    public async Task DeleteTest()
    {
        // Arrange
        var expectedStatusCode = HttpStatusCode.OK;
        var stopwatch = Stopwatch.StartNew();

        // Act
        var response = await TestHelper.HttpClient.DeleteAsync("api/product/4");

        // Assert
        await TestHelper.AssertResponseStatusCodeAsync(stopwatch, response, expectedStatusCode);
    }

    private record Product(int Id, string Name, int Price);
}