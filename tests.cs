using System.Text.RegularExpressions;
using CsharpPlaywrith.Pages;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class ExampleTest : PageTest
{
    //private IBrowser _browser;
    private LoginPage _loginPage;

    [SetUp]
    public void setup()
    {
        _loginPage = new LoginPage(Page);
    }
    


   [Test]
   //[Ignore("ignored")]
    public async Task HasTitle()
    {
        await _loginPage.GotoLogin();
        await Task.Delay(2000);
        await Expect(Page).ToHaveTitleAsync(new Regex("Swag Labs"));
    }

    [Test]
    //[Ignore("ignored")]
    public async Task AccesSiteLogin()
    {
        await _loginPage.GotoLogin();
        await _loginPage.Login("standard_user", "secret_sauce");
        await Task.Delay(2000);
        await _loginPage.ValidateLogin("standard_user");
        //await Expect(Page.Locator("span[data-test='title']")).ToHaveTextAsync("Products");

    }
    [Test]
    //[Ignore("ignored")]
    public async Task AccesDeniedLogin()
    {
        await _loginPage.GotoLogin();
        await _loginPage.Login("locked_out_user", "secret_sauce");
        await Task.Delay(2000);
        //await Expect(Page.Locator("#login_button_container > div > form > div.error-message-container.error > h3")).ToHaveTextAsync("Epic sadface: Sorry, this user has been locked out.");
        await _loginPage.ValidateLogin("locked_out_user");


    }
}