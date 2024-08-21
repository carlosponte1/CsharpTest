using CsharpPlaywrith.APIs;
using System;
using TechTalk.SpecFlow;
using WireMock.ResponseBuilders;

namespace CsharpPlaywrith.Steps
{
    [Binding]
    public class TestApiStepDefinitions : ApiTest
        
    {
        ApiTest _RealApi = new ApiTest();

        [Given(@"auth endpoint is correctly sent")]
        public async Task<HttpResponseMessage> ExecuteSendRequest()
        {
            return await SendRequestAsync();
        }

        [When(@"we recieve the response")]
        public async Task<string> ExecuteGetResponse()
        {
            var response = await ExecuteSendRequest();
            return await GetResponseContentAsync(response);
        }

        [Then(@"we validate response and status code")]
        public async Task ExecuteValidateToken()
        {
            var responseBody = await ExecuteGetResponse();
            ValidateToken(responseBody);
            //_Xb.Teardown();
        }

        
    }
}
