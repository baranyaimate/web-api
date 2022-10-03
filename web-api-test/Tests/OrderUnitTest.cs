using System.Diagnostics;
using System.Net;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;
using Xunit.Extensions.Ordering;

namespace web_api_test.Tests;

public class OrderUnitTest
{
    [Fact]
    [Order(1)]
    public async Task GetAllTest()
    {
        // Arrange
        var expectedStatusCode = HttpStatusCode.OK;
        var expectedContent = new[]
        {
            new Order(1, new User(1), new List<Product> { new(1) }),
            new Order(2, new User(2), new List<Product> { new(1), new(2) }),
            new Order(3, new User(3), new List<Product> { new(1), new(2), new(3) })
        };
        var stopwatch = Stopwatch.StartNew();

        // Act
        var response = await TestHelper.HttpClient.GetAsync("api/order");

        // Assert
        await TestHelper.AssertResponseWithContentListAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }

    [Fact]
    [Order(2)]
    public async Task GetByIdTest()
    {
        // Arrange
        var expectedStatusCode = HttpStatusCode.OK;
        var expectedContent = new Order(1, new User(1), new List<Product> { new(1) });
        var stopwatch = Stopwatch.StartNew();

        // Act
        var response = await TestHelper.HttpClient.GetAsync("api/order/" + expectedContent.Id);

        // Assert
        await TestHelper.AssertResponseWithContentListAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }

    [Fact]
    [Order(3)]
    public async Task SaveTest()
    {
        // Arrange
        var expectedStatusCode = HttpStatusCode.OK;
        var expectedContent = new Order(4, new User(1), new List<Product> { new(1) });
        var order = new OrderDto(1, 1, new[] { 1 });
        var json = JsonConvert.SerializeObject(order);
        var responseBody = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        var stopwatch = Stopwatch.StartNew();

        // Act
        var response = await TestHelper.HttpClient.PostAsync("api/order/", responseBody);

        // Assert
        await TestHelper.AssertResponseWithContentListAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }

    [Fact]
    [Order(4)]
    public async Task UpdateTest()
    {
        // Arrange
        var expectedStatusCode = HttpStatusCode.OK;
        var expectedContent = new Order(4, new User(1), new List<Product> { new(1) });
        var order = new OrderDto(1, 1, new[] { 1 });
        var json = JsonConvert.SerializeObject(order);
        var responseBody = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        var stopwatch = Stopwatch.StartNew();

        // Act
        var response = await TestHelper.HttpClient.PutAsync("api/order/" + expectedContent.Id, responseBody);

        // Assert
        await TestHelper.AssertResponseWithContentListAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }

    [Fact]
    [Order(5)]
    public async Task DeleteTest()
    {
        // Arrange
        var expectedStatusCode = HttpStatusCode.OK;
        var stopwatch = Stopwatch.StartNew();

        // Act
        var response = await TestHelper.HttpClient.DeleteAsync("api/order/4");

        // Assert
        await TestHelper.AssertResponseStatusCodeAsync(stopwatch, response, expectedStatusCode);
    }

    private record User(int Id);

    private record Product(int Id);

    private record Order(int Id, User User, List<Product> Products);

    private record OrderDto(int Id, int UserId, int[] ProductIds);
}