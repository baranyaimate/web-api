using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;
using FluentAssertions;

namespace web_api_test;

public static class TestHelper
{
    private const string JsonMediaType = "application/json";
    private const int ExpectedMaxElapsedMilliseconds = 1000;
    public static readonly HttpClient HttpClient = new() { BaseAddress = new Uri("https://localhost:7046") };

    private static readonly JsonSerializerOptions JsonSerializerOptions =
        new() { PropertyNameCaseInsensitive = true, WriteIndented = true };

    public static async Task AssertResponseWithContentAsync<T>(Stopwatch stopwatch,
        HttpResponseMessage response, HttpStatusCode expectedStatusCode,
        T expectedContent)
    {
        AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
        Assert.Equal(JsonMediaType, response.Content.Headers.ContentType?.MediaType);
        Assert.Equal(HttpStatusCode.OK, expectedStatusCode);
        Assert.Equal(expectedContent, await JsonSerializer.DeserializeAsync<T?>(
            await response.Content.ReadAsStreamAsync(),
            JsonSerializerOptions));
    }

    public static async Task AssertResponseWithContentListAsync<T>(Stopwatch stopwatch,
        HttpResponseMessage response, HttpStatusCode expectedStatusCode,
        T expectedContent)
    {
        AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
        Assert.Equal(JsonMediaType, response.Content.Headers.ContentType?.MediaType);
        Assert.Equal(HttpStatusCode.OK, expectedStatusCode);
        expectedContent.Should().BeEquivalentTo(await JsonSerializer.DeserializeAsync<T?>(
            await response.Content.ReadAsStreamAsync(),
            JsonSerializerOptions));
    }


    public static async Task AssertResponseStatusCodeAsync(Stopwatch stopwatch,
        HttpResponseMessage response, HttpStatusCode expectedStatusCode)
    {
        AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
        Assert.Equal(HttpStatusCode.OK, expectedStatusCode);
    }

    private static void AssertCommonResponseParts(Stopwatch stopwatch,
        HttpResponseMessage response, HttpStatusCode expectedStatusCode)
    {
        Assert.Equal(expectedStatusCode, response.StatusCode);
        Assert.True(stopwatch.ElapsedMilliseconds < ExpectedMaxElapsedMilliseconds);
    }

    public static StringContent GetJsonStringContent<T>(T model)
    {
        return new(JsonSerializer.Serialize(model), Encoding.UTF8, JsonMediaType);
    }
}