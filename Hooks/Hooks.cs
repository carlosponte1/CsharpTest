
using BoDi;
using Microsoft.Playwright;
using TechTalk.SpecFlow;


namespace CsharpPlaywrith.Hooks
{
    [Binding]
    public sealed class Hooks
    {

        private readonly IObjectContainer _objectContainer;
        private readonly ScenarioContext _scenarioContext;

        public Hooks(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public async Task SetupPlaywright()
        {
            var pw = await Playwright.CreateAsync();
            var browser = await pw.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            var browserContext = await browser.NewContextAsync(new BrowserNewContextOptions { BypassCSP = true });
            var page = await browserContext.NewPageAsync();
            _objectContainer.RegisterInstanceAs(browser);
            _objectContainer.RegisterInstanceAs(page);
        }
    }
}