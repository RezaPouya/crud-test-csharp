using Mc2.CrudTest.AcceptanceTests.Models;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Mc2.CrudTest.AcceptanceTests.Steps
{
    [Binding]
    public class CreateReadEditDeleteCustomerStepDefinitions
    {
        private IEnumerable<BusinessError> _businessErrors;

        internal CreateReadEditDeleteCustomerStepDefinitions()
        {
            _businessErrors = new List<BusinessError>();
        }

        [Given(@"system error codes are following")]
        public void GivenSystemErrorCodesAreFollowing(Table table)
        {
            _businessErrors = table.CreateSet<BusinessError>();
        }

        [When(@"user creates a customer with John")]
        public void WhenUserCreatesACustomerWithJohn()
        {
            throw new PendingStepException();
        }

        [When(@"Lastname of Doe")]
        public void WhenLastnameOfDoe()
        {
            throw new PendingStepException();
        }

        [When(@"Date of birth of (.*)-JAN(.*)")]
        public void WhenDateOfBirthOf_JAN(int p0, int p1)
        {
            throw new PendingStepException();
        }

        [When(@"Email of john@doe\.com")]
        public void WhenEmailOfJohnDoe_Com()
        {
            throw new PendingStepException();
        }

        [When(@"Phone number of \+(.*)")]
        public void WhenPhoneNumberOf(int p0)
        {
            throw new PendingStepException();
        }

        [When(@"Bank Account Number of IR(.*)")]
        public void WhenBankAccountNumberOfIR(int p0)
        {
            throw new PendingStepException();
        }

        [Then(@"user can lookup all customers and filter by Email of john@doe\.com and get ""([^""]*)"" records")]
        public void ThenUserCanLookupAllCustomersAndFilterByEmailOfJohnDoe_ComAndGetRecords(string p0)
        {
            throw new PendingStepException();
        }

        [When(@"user edit customer with new email of ""([^""]*)""")]
        public void WhenUserEditCustomerWithNewEmailOf(string p0)
        {
            throw new PendingStepException();
        }

        [Then(@"user can lookup all customers and filter by Email of ""([^""]*)"" and get ""([^""]*)"" records")]
        public void ThenUserCanLookupAllCustomersAndFilterByEmailOfAndGetRecords(string p0, string p1)
        {
            throw new PendingStepException();
        }

        [When(@"user delete customer by Email of ""([^""]*)""")]
        public void WhenUserDeleteCustomerByEmailOf(string p0)
        {
            throw new PendingStepException();
        }

        [Then(@"user can lookup customer by Email of ""([^""]*)"" and get ""([^""]*)"" records")]
        public void ThenUserCanLookupCustomerByEmailOfAndGetRecords(string p0, string p1)
        {
            throw new PendingStepException();
        }

        [Then(@"user can lookup customer by Email of john@doe\.com and get ""([^""]*)"" records")]
        public void ThenUserCanLookupCustomerByEmailOfJohnDoe_ComAndGetRecords(string p0)
        {
            throw new PendingStepException();
        }
    }
}
