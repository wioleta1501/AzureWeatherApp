using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class APITests : PlaywrightTest
{

    private IAPIRequestContext Request = null;

    [Test]
    public async Task ShouldSeeWeather()
    {
        var req = await Request.GetAsync("/WeatherForecast/");
        Assert.True(req.Ok);
        var issuesJsonResponse = await req.JsonAsync();
        JsonElement? issue = null;
        foreach (JsonElement issueObj in issuesJsonResponse?.EnumerateArray())
        {
            if (issueObj.TryGetProperty("date", out var title) == true)
            {
                if (title.GetString() == "2023-06-10")
                {
                    issue = issueObj;
                }
            }
        }
        Assert.NotNull(issue);
    }

    [SetUp]
    public async Task SetUpAPITesting()
    {
        await CreateAPIRequestContext();
    }

    private async Task CreateAPIRequestContext()
    {
        var headers = new Dictionary<string, string>();
        headers.Add("Accept", "application/json");

        Request = await this.Playwright.APIRequest.NewContextAsync(new()
        {
            BaseURL = "http://localhost:5153",
            ExtraHTTPHeaders = headers,
        });
    }


    [TearDown]
    public async Task TearDownAPITesting()
    {
        await Request.DisposeAsync();
    }
}