using CsharpPlaywrith.APIs;
using System;
using TechTalk.SpecFlow;
using WireMockExample;

namespace CsharpPlaywrith.Steps
{
    [Binding]
    public class MockApiStepDefinitions : MockApiTests
    {
        MockApiTests _ApiMock = new MockApiTests();

        [Given(@"auth endpoint its setting up")]
        public async Task GivenAuthEndpointItsSettingUp()
        {
            await _ApiMock.StartServer();
            
        }

        [When(@"auth endpoint is sent")]
        public async void WhenAuthEndpointIsSent()
        {
            await _ApiMock.SendAuthRequest();
            
        }

        [Then(@"response has correct token and status")]
        public async void ThenResponseHasCorrectTokenAndStatus()
        {
            await _ApiMock.VerifyResponse();
            
        }
        [AfterScenario]
        public void AfterScenario()
        {
            _ApiMock.TearDown();
        }
    }
}
