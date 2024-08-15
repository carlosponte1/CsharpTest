using CsharpPlaywrith.APIs;
using System;
using TechTalk.SpecFlow;
using WireMockExample;

namespace CsharpPlaywrith.Steps
{
    [Binding]
    public class MockApiStepDefinitions : ApiTests
    {
        ApiTests _Xa = new ApiTests();

        [Given(@"auth endpoint its setting up")]
        public async Task GivenAuthEndpointItsSettingUp()
        {
            await _Xa.StartServer();
            
        }

        [When(@"auth endpoint is sent")]
        public async void WhenAuthEndpointIsSent()
        {
            await _Xa.SendAuthRequest();
            
        }

        [Then(@"response has correct token and status")]
        public async void ThenResponseHasCorrectTokenAndStatus()
        {
            await _Xa.VerifyResponse();
            
        }
        [AfterScenario]
        public void AfterScenario()
        {
            _Xa.TearDown();
        }
    }
}
