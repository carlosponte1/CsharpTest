using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpPlaywrith.Pages
{
    internal class LoginPage : PageTest
    {
        string usernameLocator = "input[data-test='username']";
        string passwordLocator = "input[data-test='password']";
        string loginButtonLocator = "input[data-test='login-button']";
        public readonly IPage _page;
        public LoginPage(IPage page)
        {
            _page = page;
        }
        private ILocator Username => _page.Locator(usernameLocator);
        private ILocator Password => _page.Locator(passwordLocator);
        private ILocator LoginButton => _page.Locator(loginButtonLocator);

        //private ILocator CorrectLogin => _page.Locator("span[data-test='title']");


        public async Task GotoLogin()
        {
            await _page.GotoAsync("https://www.saucedemo.com/");
        }
        
        public async Task Login(string username, string password)
        {
            
            await Username.FillAsync(username);
            await Password.FillAsync(password);
            await Task.Delay(2000);
            await LoginButton.ClickAsync();
        }

       /* public async Task ValidateLogin1(string text)
        {  if(text == "standard_user")
            {
                await Task.Delay(2000);
                await Expect(_page.Locator("span[data-test='title']")).ToHaveTextAsync("Products");
            }
            else if (text == "locked_out_user")
            {
                await Task.Delay(2000);
                await Expect(Page.Locator("#login_button_container > div > form > div.error-message-container.error > h3")).ToHaveTextAsync("Epic sadface: Sorry, this user has been locked out.");
            }
            
            
        }*/

        public async Task<string> ValidateLogin(string username)
        {
            if (username == "standard_user")
            {
                
                // Esperar hasta que el título de la página sea "Products"
               // await _page.Locator("span[data-test='title']").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
                //await Expect(_page.Locator("span[data-test='title']")).ToHaveTextAsync("Products");
                return await _page.Locator("span[data-test='title']").TextContentAsync();

            }
            else 
            {
                // Esperar hasta que el mensaje de error sea visible
               // await _page.Locator("#login_button_container > div > form > div.error-message-container.error > h3").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
                //await Expect(_page.Locator("#login_button_container > div > form > div.error-message-container.error > h3")).ToHaveTextAsync("Epic sadface: Sorry, this user has been locked out.");
                return await _page.Locator("#login_button_container > div > form > div.error-message-container.error > h3").TextContentAsync();
            }

        }

    }
}


   
