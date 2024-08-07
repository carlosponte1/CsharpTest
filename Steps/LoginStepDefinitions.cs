using CsharpPlaywrith.Pages;
using Microsoft.Playwright;
using System;
using TechTalk.SpecFlow;
using Microsoft.Playwright.NUnit;
using FluentAssertions;


//using Microsoft.Playwright;
//using Microsoft.Playwright.NUnit;


namespace CsharpPlaywrith.Steps
{
    [Binding]
    public class LoginStepDefinitions 
    {
        private readonly LoginPage _loginPage;
        public readonly IPage _page;
       

        //private LoginPage _loginPage;


        public LoginStepDefinitions(IPage page) 
        {

            _page = page;
            _loginPage = new LoginPage(_page);

        }
        
        
        [Given(@"I Wanto to go the Login Page")]
            public async Task GivenIWantoToGoTheLoginPage()
            {
            await _loginPage.GotoLogin();
            }

            [When(@"I want to login with the user ""([^""]*)""")]
            public async Task WhenIWantToLoginWithTheUser(string p0)
            {
            await _loginPage.Login(p0, "secret_sauce");
            }

            [Then(@"login success with  ""([^""]*)""")]
            public async Task ThenLoginSuccessWith(string p0)
            
                {if (p0 == "standard_user")
            {

                var text = await _loginPage.ValidateLogin(p0);
                text.Should().Be("Products");
            }
            else if (p0 == "locked_out_user")
            {
                var text = await _loginPage.ValidateLogin(p0);
                text.Should().Be("Epic sadface: Sorry, this user has been locked out.");
            }
            else
            {
                return;
            }
            

        }
        }
    } 


