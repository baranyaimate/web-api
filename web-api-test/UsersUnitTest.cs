using System.Diagnostics;

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
            new User(6, "user#1", "user#1", "user#1"),
            new User(7, "user#2", "user#2", "user#2")
        };
        var stopwatch = Stopwatch.StartNew();

        // Act
        var response = await _httpClient.GetAsync("api/User");
        
        // Assert
        await TestHelper.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }
    
    [Fact]
    public async void GetByIdTest()
    {
        // Arrange
        var expectedStatusCode = System.Net.HttpStatusCode.OK;
        var expectedContent = new User(6, "user#1", "user#1", "user#1");
        var stopwatch = Stopwatch.StartNew();

        // Act
        var response = await _httpClient.GetAsync("api/User/6");
        
        // Assert
        await TestHelper.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }
}