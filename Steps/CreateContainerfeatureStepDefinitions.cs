using CsharpPlaywrith.APIs;
using CsharpPlaywrith.TestContainers;
using System;
using TechTalk.SpecFlow;

namespace CsharpPlaywrith.Steps 
{
    [Binding]
    public class CreateContainerfeatureStepDefinitions : MySqlCont

    {
        MySqlCont _mysql = new MySqlCont();

        [Given(@"I have a runnig MySQL container")]
        public async Task GivenIHaveARunnigMySQLContainer()
        {
           await _mysql.SqlContStart();
            //throw new PendingStepException();
        }

        [When(@"I connect to the Database container")]
        public async Task WhenIConnectToTheDatabaseContainer()
        {
           await _mysql.SqlOpen();
           // throw new PendingStepException();
        }

        [Then(@"The Database should be accesible")]
        public async Task ThenTheDatabaseShouldBeAccesible()
        {
           await _mysql.ValidateCon();
            //await _mysql.closeCon();

           // throw new PendingStepException();
        }
    }
}
