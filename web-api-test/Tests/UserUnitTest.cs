using System.Diagnostics;
using System.Net;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;
using Xunit.Extensions.Ordering;

namespace web_api_test.Tests;

public class UserTest
{
    [Fact]
    [Order(1)]
    public async Task GetAllTest()
    {
        // Arrange
        var expectedStatusCode = HttpStatusCode.OK;
        var expectedContent = new[]
        {
            new User(1, "Johanna", "Watts", "johanna.watts@gmail.com"),
            new User(2, "Bryce", "Cooper", "bryce.cooper@gmail.com"),
            new User(3, "Lilah", "Davis", "lilah.davis@gmail.com")
        };
        var stopwatch = Stopwatch.StartNew();

        // Act
        var response = await TestHelper.HttpClient.GetAsync("api/user");

        // Assert
        await TestHelper.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }

    [Fact]
    [Order(2)]
    public async Task GetByIdTest()
    {
        // Arrange
        var expectedStatusCode = HttpStatusCode.OK;
        var expectedContent = new User(1, "Johanna", "Watts", "johanna.watts@gmail.com");
        var stopwatch = Stopwatch.StartNew();

        // Act
        var response = await TestHelper.HttpClient.GetAsync("api/user/" + expectedContent.Id);

        // Assert
        await TestHelper.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }

    [Fact]
    [Order(3)]
    public async Task SaveTest()
    {
        // Arrange
        var expectedStatusCode = HttpStatusCode.OK;
        var expectedContent = new User(4, "Simon", "Price", "simon.price@gmail.com");
        var json = JsonConvert.SerializeObject(expectedContent);
        var responseBody = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        var stopwatch = Stopwatch.StartNew();

        // Act
        var response = await TestHelper.HttpClient.PostAsync("api/user/", responseBody);

        // Assert
        await TestHelper.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }

    [Fact]
    [Order(4)]
    public async Task UpdateTest()
    {
        // Arrange
        var expectedStatusCode = HttpStatusCode.OK;
        var expectedContent = new User(4, "Simon", "Hudson", "simon.hudson@gmail.com");
        var json = JsonConvert.SerializeObject(expectedContent);
        var responseBody = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        var stopwatch = Stopwatch.StartNew();

        // Act
        var response = await TestHelper.HttpClient.PutAsync("api/user/" + expectedContent.Id, responseBody);

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
        var response = await TestHelper.HttpClient.DeleteAsync("api/user/4");

        // Assert
        await TestHelper.AssertResponseStatusCodeAsync(stopwatch, response, expectedStatusCode);
    }

    private record User(int Id, string FirstName, string LastName, string Email);
}