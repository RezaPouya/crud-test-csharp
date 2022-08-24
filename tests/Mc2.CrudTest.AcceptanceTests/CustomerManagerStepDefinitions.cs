using FluentAssertions;
using Mc2.CrudTest.AcceptanceTests.Drivers;
using Mc2.CrudTest.AcceptanceTests.Models;
using Mc2.CrudTest.AcceptanceTests.Models.DTOs;
using TechTalk.SpecFlow.Assist;


namespace Mc2.CrudTest.AcceptanceTests
{
    [Binding]
    public class CustomerManagerStepDefinitions
    {
        private readonly WebClient _webClient;
        private readonly CustomerInputRequest _customerInputRequest;
        private readonly ScenarioContext _scenarioContext;
        private IEnumerable<BusinessError> _businessErrors = new List<BusinessError>();

        public CustomerManagerStepDefinitions(WebClient webClient, ScenarioContext scenarioContext)
        {
            _webClient = webClient;
            _scenarioContext = scenarioContext;
            _customerInputRequest = new CustomerInputRequest();
        }

        [Given(@"system error codes are following")]
        public void GivenSystemErrorCodesAreFollowing(Table table)
        {
            _businessErrors = table.CreateSet<BusinessError>();
        }

        [Given(@"we have a customer with Firstname of (.*)")]
        public void GivenWeHaveACustomerWithFirstname(string firstName)
        {
            _customerInputRequest.FirstName = firstName;
        }

        [Given(@"Lastname of (.*)")]
        public void GivenLastname(string lastName)
        {
            _customerInputRequest.LastName = lastName;
        }

        [Given(@"Date of birth of (.*)")]
        public void GivenDateOfBirthOfJan(string dateOfBirth)
        {
            _customerInputRequest.DateOfBirth = dateOfBirth;
        }

        [Given(@"Email of (.*)")]
        public void GivenEmail(string email)
        {
            _customerInputRequest.Email = email;
        }

        [Given(@"Phone number of (.*)")]
        public void GivenPhoneNumberOf(string phoneNumber)
        {
            _customerInputRequest.PhoneNumber = phoneNumber;
        }

        [Given(@"Bank Account Number of (.*)")]
        public void GivenBankAccountNumberOfIr(string bankAccountNumber)
        {
            _customerInputRequest.BankAccountNumber = bankAccountNumber;
        }

        [When(@"user create customer with given info")]
        public async Task WhenUserCreateCustomerWithGivenInfo()
        {
            var response = await _webClient.CreateCustomer(_customerInputRequest);
            response.Should().BeTrue();
        }

        [Then(@"user can lookup all customers and filter by Email of john@doe\.com and get ""([^""]*)"" records")]
        public async Task ThenUserCanLookupAllCustomersAndFilterByEmailOfJohnDoe_ComAndGetRecords(string count)
        {
            var customers = await _webClient.GetAllCustomer();
            var customerWithJhoneDoeEmails = customers.Where(p => p.Email.Equals("john@doe.com")).ToList();
            var countOfExceptedRecord = ToNumber(count);
            customerWithJhoneDoeEmails.Should().HaveCount(countOfExceptedRecord);
        }

        [When(@"user edit customer with new email of ""([^""]*)""")]
        public async Task WhenUserEditCustomerWithNewEmailOf(string email)
        {
            var customer = await _webClient.GetCustomerCustomerByEmail("john@doe.com");
            customer.Should().NotBeNull();
            customer.Email = email;
            var response = await _webClient.UpdateCustomer(new CustomerInputRequest(customer));
            response.Should().BeTrue();
        }

        [Then(@"user can lookup all customers and filter by Email of ""([^""]*)"" and get ""([^""]*)"" records")]
        public async Task ThenUserCanLookupAllCustomersAndFilterByEmailOfAndGetRecords(string email, string count)
        {
            var customers = await _webClient.GetAllCustomer();
            var customerWithJhoneDoeEmails = customers.Where(p => p.Email.Equals(email)).ToList();
            var countOfExceptedRecord = ToNumber(count);
            customerWithJhoneDoeEmails.Should().HaveCount(countOfExceptedRecord);
        }

        [When(@"user delete customer by Email of ""([^""]*)""")]
        public async Task WhenUserDeleteCustomerByEmailOf(string email)
        {
            var customer = await _webClient.GetCustomerCustomerByEmail(email);
            customer.Should().NotBeNull();
            var response = await _webClient.DeleteCustomer(customer.Id);
            response.Should().BeTrue();
        }

        public byte ToNumber(string count)
        {
            return Convert.ToByte(count);
        }
    }
}
