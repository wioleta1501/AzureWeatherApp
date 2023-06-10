using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class UITests : PageTest
{
    [Test]
    public async Task HomepageHasTitle()
    {
        await Page.GotoAsync("http://localhost:5153/swagger/index.html");

        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync(new Regex("Swagger UI"));

        // create a locator
        //var getStarted = Page.GetByRole(AriaRole.Link, new() { Name = "Get started" });

        // Expect an attribute "to be strictly equal" to the value.
        //await Expect(getStarted).ToHaveAttributeAsync("href", "/docs/intro");

        // Click the get started link.
        //await getStarted.ClickAsync();

        // Expects the URL to contain intro.
        //await Expect(Page).ToHaveURLAsync(new Regex(".*intro"));
    }
}