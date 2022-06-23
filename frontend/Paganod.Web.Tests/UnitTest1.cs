using Microsoft.Playwright;

using System.Net.Http;
using System.Threading.Tasks;

using Xunit;

namespace Paganod.Web.Tests;

//Testing:

//- Create Tests using Playwright
//- Follow https://github.com/JD-Innovensa/InMemoryPlaywrightDemo to run entirely in memory (use Entity Framework core as inmemory backend)
//- Moq Httpclient(fake delay in SendAsync() on HttpMessageHandler) -- https://www.thecodebuzz.com/mock-httpclient-with-messagehandler-unit-test/

public class UnitTest1 : IClassFixture<WebApplicationFactoryFixture>
{
    public UnitTest1(WebApplicationFactoryFixture factoryFixture)
    {
        factoryFixture.CreateDefaultClient();
    }

    [Fact]
    public async Task Test1()
    {
        //var testBlazorServer = new TestBlazorServer();
        //var http = testBlazorServer.CreateDefaultClient();


        using var playwright = await Playwright.CreateAsync();
        var chromium = playwright.Chromium;

        var browser = await chromium.LaunchAsync(new BrowserTypeLaunchOptions { Channel = "chrome", Headless = false });
        //await using var browser = await chromium.LaunchAsync(new BrowserTypeLaunchOptions
        //{
        //    Headless = false,
        //    SlowMo = 1000
        //});
        var context = await browser.NewContextAsync();
        // Open new page
        var page = await context.NewPageAsync();
        // Go to https://localhost:7048/
        await page.GotoAsync("https://localhost:7048/data/transactions");
        // Click text=Counter
        //await page.ClickAsync("text=Counter");
        // Assert.AreEqual("https://localhost:7048/data/transactions", page.Url);
        // Click text=Current count: 0
        await page.ClickAsync("text=Current count: 0");
        // Click text=Click me
        await page.ClickAsync("text=Click me");
        // Click text=Current count: 1
        await page.ClickAsync("text=Current count: 1");
    }
}