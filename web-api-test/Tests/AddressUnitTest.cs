using System.Diagnostics;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;
using Xunit.Extensions.Ordering;

namespace web_api_test.Tests;

public class AddressUnitTest
{
    // private record User(int Id, string FirstName, string LastName, string Email);
    private record Address(int Id, string Country, string City, string Postcode, string State, string StreetName, string StreetNumber, int UserId);
    
    [Fact, Order(1)]
    public async void GetAllTest()
    {
        // Arrange
        var expectedStatusCode = System.Net.HttpStatusCode.OK;
        var expectedContent = new[]
        {
            new Address(1, "United States", "Rose Hill", "67133", "Kansas", "Henery Street", "990", 1),
            new Address(2, "United States", "Louisville", "40203", "Kentucky", "Gregory Lane", "4542", 2),
            new Address(3, "United States", "Finchville", "40022", "Kentucky", "Karen Lane", "3083", 3),
        };
        var stopwatch = Stopwatch.StartNew();

        // Act
        var response = await TestHelper.HttpClient.GetAsync("api/address");
        
        // Assert
        await TestHelper.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }
    
    [Fact, Order(2)]
    public async void GetByIdTest()
    {
        // Arrange
        var expectedStatusCode = System.Net.HttpStatusCode.OK;
        var expectedContent = new Address(1, "United States", "Rose Hill", "67133", "Kansas", "Henery Street", "990", 1);
        var stopwatch = Stopwatch.StartNew();

        // Act
        var response = await TestHelper.HttpClient.GetAsync("api/address/" + expectedContent.Id);
        
        // Assert
        await TestHelper.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }

    [Fact, Order(3)]
    public async void SaveTest()
    {
        // Arrange
        var expectedStatusCode = System.Net.HttpStatusCode.OK;
        var expectedContent = new Address(4, "United States", "Tyler", "75757", "Texas", "Gladwell Street", "2586", 1);
        var json = JsonConvert.SerializeObject(expectedContent);
        var responseBody = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        var stopwatch = Stopwatch.StartNew();
        
        // Act
        var response = await TestHelper.HttpClient.PostAsync("api/address/", responseBody);

        // Assert
        await TestHelper.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
    }

    [Fact, Order(4)]
    public async void UpdateTest()
    {
        // Arrange
        var expectedStatusCode = System.Net.HttpStatusCode.OK;
        var expectedContent = new Address(4, "United States", "Tyler", "75757", "Texas", "Garfield Road", "342", 1);
        var json = JsonConvert.SerializeObject(expectedContent);
        var responseBody = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        var stopwatch = Stopwatch.StartNew();
        
        // Act
        var response = await TestHelper.HttpClient.PutAsync("api/address/" + expectedContent.Id, responseBody);

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
        var response = await TestHelper.HttpClient.DeleteAsync("api/address/4");

        // Assert
        await TestHelper.AssertResponseStatuCodeAsync(stopwatch, response, expectedStatusCode);
    }
    
}