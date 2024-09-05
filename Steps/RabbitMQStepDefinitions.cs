using CsharpPlaywrith.TestContainers;
using System;
using TechTalk.SpecFlow;

namespace CsharpPlaywrith.Steps
{
    [Binding]
    public class RabbitMQStepDefinitions
    {

        RabbitmqCont _RabbitMQsteps = new RabbitmqCont();

        [Given(@"A running and ready container")]
        public async Task GivenARunningAndReadyContainer()
        {
           await _RabbitMQsteps.starRabbitmqCont();
            //throw new PendingStepException();
        }

        [When(@"the producer send a message")]
        public void WhenTheProducerSendAMessage()
        {
            _RabbitMQsteps.SendMessage("Hola conejito");
            //throw new PendingStepException();
        }

        [Then(@"the consumer recieve a message")]
        public void ThenTheConsumerRecieveAMessage()
        {
            _RabbitMQsteps.ReceiveMessage();
            //throw new PendingStepException();
        }
    }
}
