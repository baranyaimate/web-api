using System.Diagnostics;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;

namespace web_api_test;

public class UserTest
{
    
    private readonly HttpClient _httpClient = new() { BaseAddress = new Uri("https://localhost:7046") };

    private record User(int Id, string FirstName, string LastName, string Email);
    
    [Fact]
    public async void GetAllTest()
    {
        // Arrange
        var expectedStatusCode = System.Net.HttpStatusCode.OK;
        var expectedContent = new[]
        {
            new User(1, "Johanna", "Watts", "johanna.watts@gmail.com"),
            new User(2, "Bryce", "Cooper", "bryce.cooper@gmail.com"),
            new User(3, "Lilah", "Davis", "lilah.davis@gmail.com"),
        };
        var stopwatch = Stopwatch.StartNew();

        // Act
        var response = await _httpClient.GetAsync("api/user");
        
        // Assert
        await TestHelper.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }
    
    [Fact]
    public async void GetByIdTest()
    {
        // Arrange
        var expectedStatusCode = System.Net.HttpStatusCode.OK;
        var expectedContent = new User(1, "Johanna", "Watts", "johanna.watts@gmail.com");
        var stopwatch = Stopwatch.StartNew();

        // Act
        var response = await _httpClient.GetAsync("api/user/" + expectedContent.Id);
        
        // Assert
        await TestHelper.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }

    [Fact]
    public async void SaveTest()
    {
        // Arrange
        var expectedStatusCode = System.Net.HttpStatusCode.OK;
        var expectedContent = new User(4, "Simon", "Price", "simon.price@gmail.com");
        var json = JsonConvert.SerializeObject(expectedContent);
        var responseBody = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        var stopwatch = Stopwatch.StartNew();
        
        // Act
        var response = await _httpClient.PostAsync("api/user/", responseBody);

        // Assert
        await TestHelper.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }

    [Fact]
    public async void UpdateTest()
    {
        // Arrange
        var expectedStatusCode = System.Net.HttpStatusCode.OK;
        var expectedContent = new User(4, "Simon", "Hudson", "simon.hudson@gmail.com");
        var json = JsonConvert.SerializeObject(expectedContent);
        var responseBody = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        var stopwatch = Stopwatch.StartNew();
        
        // Act
        var response = await _httpClient.PutAsync("api/user/" + expectedContent.Id, responseBody);

        // Assert
        await TestHelper.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }

    [Fact]
    public async void DeleteTest()
    {
        // Arrange
        var expectedStatusCode = System.Net.HttpStatusCode.OK;
        var expectedContent = "The user was deleted";
        var stopwatch = Stopwatch.StartNew();
        
        // Act
        var response = await _httpClient.DeleteAsync("api/user/4");

        // Assert
        await TestHelper.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }
}