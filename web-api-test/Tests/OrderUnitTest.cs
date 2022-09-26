using System.Diagnostics;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;
using Xunit.Extensions.Ordering;

namespace web_api_test.Tests;

public class OrderUnitTest
{
    private record Order(int Id, string FirstName, string LastName, string Email);
    
    [Fact, Order(1)]
    public async void GetAllTest()
    {
        // Arrange
        var expectedStatusCode = System.Net.HttpStatusCode.OK;
        var expectedContent = new[]
        {
            new Order(1, "Johanna", "Watts", "johanna.watts@gmail.com"),
            new Order(2, "Bryce", "Cooper", "bryce.cooper@gmail.com"),
            new Order(3, "Lilah", "Davis", "lilah.davis@gmail.com"),
        };
        var stopwatch = Stopwatch.StartNew();

        // Act
        var response = await TestHelper.HttpClient.GetAsync("api/order");
        
        // Assert
        await TestHelper.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }
    
    [Fact, Order(2)]
    public async void GetByIdTest()
    {
        // Arrange
        var expectedStatusCode = System.Net.HttpStatusCode.OK;
        var expectedContent = new Order(1, "Johanna", "Watts", "johanna.watts@gmail.com");
        var stopwatch = Stopwatch.StartNew();

        // Act
        var response = await TestHelper.HttpClient.GetAsync("api/order/" + expectedContent.Id);
        
        // Assert
        await TestHelper.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }

    [Fact, Order(3)]
    public async void SaveTest()
    {
        // Arrange
        var expectedStatusCode = System.Net.HttpStatusCode.OK;
        var expectedContent = new Order(4, "Simon", "Price", "simon.price@gmail.com");
        var json = JsonConvert.SerializeObject(expectedContent);
        var responseBody = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        var stopwatch = Stopwatch.StartNew();
        
        // Act
        var response = await TestHelper.HttpClient.PostAsync("api/order/", responseBody);

        // Assert
        await TestHelper.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }

    [Fact, Order(4)]
    public async void UpdateTest()
    {
        // Arrange
        var expectedStatusCode = System.Net.HttpStatusCode.OK;
        var expectedContent = new Order(4, "Simon", "Hudson", "simon.hudson@gmail.com");
        var json = JsonConvert.SerializeObject(expectedContent);
        var responseBody = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        var stopwatch = Stopwatch.StartNew();
        
        // Act
        var response = await TestHelper.HttpClient.PutAsync("api/order/" + expectedContent.Id, responseBody);

        // Assert
        await TestHelper.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }

    [Fact, Order(5)]
    public async void DeleteTest()
    {
        // Arrange
        var expectedStatusCode = System.Net.HttpStatusCode.OK;
        var stopwatch = Stopwatch.StartNew();
        
        // Act
        var response = await TestHelper.HttpClient.DeleteAsync("api/order/4");

        // Assert
        await TestHelper.AssertResponseStatusCodeAsync(stopwatch, response, expectedStatusCode);
    }
}