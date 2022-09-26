using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace web_api_test;

public static class TestHelper
{
    public static readonly HttpClient HttpClient = new() { BaseAddress = new Uri("https://localhost:7046") };
    
    private const string JsonMediaType = "application/json";
    private const int ExpectedMaxElapsedMilliseconds = 1000;
    private static readonly JsonSerializerOptions JsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };
    
    public static async Task AssertResponseWithContentAsync<T>(Stopwatch stopwatch,
        HttpResponseMessage response, System.Net.HttpStatusCode expectedStatusCode,
        T expectedContent)
    {
        AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
        Assert.Equal(JsonMediaType, response.Content.Headers.ContentType?.MediaType);
        Assert.Equal(System.Net.HttpStatusCode.OK, expectedStatusCode);
        Assert.Equal(expectedContent, await JsonSerializer.DeserializeAsync<T?>(
            await response.Content.ReadAsStreamAsync(), JsonSerializerOptions));
    }
    
    public static async Task AssertResponseStatusCodeAsync(Stopwatch stopwatch,
        HttpResponseMessage response, System.Net.HttpStatusCode expectedStatusCode)
    {
        AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
        Assert.Equal(System.Net.HttpStatusCode.OK, expectedStatusCode);
    }
    
    private static void AssertCommonResponseParts(Stopwatch stopwatch,
        HttpResponseMessage response, System.Net.HttpStatusCode expectedStatusCode)
    {
        Assert.Equal(expectedStatusCode, response.StatusCode);
        Assert.True(stopwatch.ElapsedMilliseconds < ExpectedMaxElapsedMilliseconds);
    }
    public static StringContent GetJsonStringContent<T>(T model)
        => new(JsonSerializer.Serialize(model), Encoding.UTF8, JsonMediaType);
}