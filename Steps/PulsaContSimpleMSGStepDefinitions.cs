using CsharpPlaywrith.TestContainers;
using System;
using TechTalk.SpecFlow;

namespace CsharpPlaywrith.Steps
{
    [Binding]
    public class PulsaContSimpleMSGStepDefinitions 
    {

        PulsarContA _PulsarContA = new PulsarContA();

        [Given(@"a running pulsar container")]
        public async Task GivenARunningPulsarContainer()
        {
            await _PulsarContA.StartPulsarCon();
            await _PulsarContA.CreateProdCons();
            
            //throw new PendingStepException();
        }

        [When(@"a message is sent")]
        public async Task WhenAMessageIsSent()
        {
            await _PulsarContA.SendMessage();
           // throw new PendingStepException();
        }

        [Then(@"the message is successfully received")]
        public async Task ThenTheMessageIsSuccessfullyReceived()
        {
            await _PulsarContA.ReceiveMessage();
            //throw new PendingStepException();
        }
    }
}
