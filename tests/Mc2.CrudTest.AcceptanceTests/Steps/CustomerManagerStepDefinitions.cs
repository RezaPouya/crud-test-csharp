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
        private readonly CustomerWebClientAgent _webClient;
        private CustomerInputRequest _customerInputRequest;
        private CustomerInputRequest _invalidCellphoneInputRequest;
        private readonly ScenarioContext _scenarioContext;
        private IEnumerable<BusinessError> _businessErrors = new List<BusinessError>();

        public CustomerManagerStepDefinitions(CustomerWebClientAgent webClient, ScenarioContext scenarioContext)
        {
            _webClient = webClient;
            _scenarioContext = scenarioContext;
        }

        [Given(@"system error codes are following")]
        public void GivenSystemErrorCodesAreFollowing(Table table)
        {
            _businessErrors = table.CreateSet<BusinessError>();
        }

        [Given(@"there is initial customer with these info")]
        public async Task GivenThereIsInitialCustomerWithTheseInfo(Table table)
        {
            var req = table.CreateSet<CustomerInputRequest>().FirstOrDefault();
            await _webClient.CreateCustomer(req);
            var getResponse = await _webClient.GetCustomerCustomerByEmail(req.Email);
            getResponse.Should().NotBeNull();
            _scenarioContext.Set<ApiResult<CustomerOutputResponse>>(getResponse , "initial_customer");
        }

        [Given(@"we have a customer with Firstname of (.*)")]
        public void GivenWeHaveACustomerWithFirstname(string firstName)
        {
            _customerInputRequest = new CustomerInputRequest();
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
            response.Should().NotBeNull();
            response.StatusCode.Equals(200);
            response.IsSuccess.Should().BeTrue();
        }

        [Then(@"user can lookup all customers and filter by Email of john@doe\.com and get ""([^""]*)"" records")]
        public async Task ThenUserCanLookupAllCustomersAndFilterByEmailOfJohnDoe_ComAndGetRecords(string count)
        {
            List<CustomerOutputResponse> specifiedCustomers =
                await GetAllCustomerAndFilterSpecifiedCustomerFromApis("john@doe.com");
            var countOfExceptedRecord = ToNumber(count);
            specifiedCustomers.Should().HaveCount(countOfExceptedRecord);
        }

        [When(@"user edit customer with new email of ""([^""]*)""")]
        public async Task WhenUserEditCustomerWithNewEmailOf(string email)
        {
            var customer = await _webClient.GetCustomerCustomerByEmail("john@doe.com");
            customer.Should().NotBeNull();
            customer.Data.Should().NotBeNull();
            customer.Data.Email = email;
            var response = await _webClient.UpdateCustomer(new CustomerInputRequest(customer.Data));
            response.Should().NotBeNull();
            response.StatusCode.Should().Be("200");
            response.IsSuccess.Should().BeTrue();
        }

        [Then(@"user can lookup all customers and filter by Email of ""([^""]*)"" and get ""([^""]*)"" records")]
        public async Task ThenUserCanLookupAllCustomersAndFilterByEmailOfAndGetRecords(string email, string count)
        {
            var specifiedCustomers = await GetAllCustomerAndFilterSpecifiedCustomerFromApis(email);
            var countOfExceptedRecord = ToNumber(count);
            specifiedCustomers.Should().HaveCount(countOfExceptedRecord);
        }

        [When(@"user delete customer by Email of ""([^""]*)""")]
        public async Task WhenUserDeleteCustomerByEmailOf(string email)
        {
            var customer = await _webClient.GetCustomerCustomerByEmail(email);
            customer.Should().NotBeNull();
            customer.Data.Should().NotBeNull();
            var response = await _webClient.DeleteCustomer(customer.Data.Id);
            response.Should().NotBeNull();
            response.StatusCode.Should().Be("200");
            response.IsSuccess.Should().BeTrue();
        }

        [Given(@"user have a customer with these info")]
        public void GivenWeHaveACustomerWithTheseInfo(Table table)
        {
            _invalidCellphoneInputRequest = table.CreateSet<CustomerInputRequest>().FirstOrDefault();
        }

        [Then(@"when we try to create customer, it should fail")]
        public async Task ThenWhenWeTryToCreateCustomerItShouldFail()
        {
            var response = await _webClient.CreateCustomer(_invalidCellphoneInputRequest);
            response.Should().NotBeNull();
            _scenarioContext.Set<ApiResult>(response, "invalidCellphone_Create_Request_Response");
        }

        [Then(@"error message should be ""([^""]*)""")]
        public void ThenErrorMessageShouldBe(string errorMessage)
        {
            var response = (ApiResult)_scenarioContext["invalidCellphone_Create_Request_Response"];
            response.Should().NotBeNull();
            response.IsSuccess.Should().BeFalse();
            response.Errors.Should().HaveCountGreaterThan(0);
            response.Errors.ContainsValue(errorMessage);
        }

        [When(@"user try to update Foo Bar customer, with Invalid Phone number of ""([^""]*)""")]
        public async Task WhenUserTryToUpdateFooBarCustomerWithInvalidPhoneNumberOf(string phoneNumber)
        {
            var fooBarCustomer = (ApiResult<CustomerOutputResponse>)_scenarioContext["initial_customer"];
            fooBarCustomer.Should().NotBeNull();
            fooBarCustomer.Data.Should().NotBeNull();
            var req = new CustomerInputRequest(fooBarCustomer.Data);
            req.PhoneNumber = phoneNumber;
            var response = await _webClient.UpdateCustomer(req);
            response.Should().NotBeNull();
            _scenarioContext.Set<ApiResult>(response, "invalidCellphone_Update_Request_Response");
        }

        [Then(@"the thrown error message on update should be ""([^""]*)""")]
        public void ThenTheThrownErrorMessageOnUpdateShouldBe(string errorMessage)
        {
            var response = (ApiResult)_scenarioContext["invalidCellphone_Update_Request_Response"];
            response.Should().NotBeNull();
            response.IsSuccess.Should().BeFalse();
            response.Errors.Should().HaveCountGreaterThan(0);
            response.Errors.ContainsValue(errorMessage);
        }

        [When(@"user try to create duplicated customer with these info")]
        public async Task WhenUserTryToCreateDuplicatedCustomerWithTheseInfo(Table table)
        {
            var req = table.CreateSet<CustomerInputRequest>().FirstOrDefault();
            var response = await _webClient.CreateCustomer(req);
            response.Should().NotBeNull();
            _scenarioContext.Set<ApiResult>(response, "duplicated_customer_info_request_response");
        }

        [Then(@"customer duplicated error message should be ""([^""]*)""")]
        public async Task ThenCustomerDuplicatedErrorMessageShouldBe(string errorMessage)
        {
            var response = (ApiResult)_scenarioContext["duplicated_customer_info_request_response"];
            response.Should().NotBeNull();
            response.IsSuccess.Should().BeFalse();
            response.Message.Equals(errorMessage);
        }


        [When(@"user try to create duplicated customer with duplicated email and these info")]
        public async Task WhenUserTryToCreateDuplicatedCustomerWithDuplicatedEmailAndTheseInfo(Table table)
        {
            var req = table.CreateSet<CustomerInputRequest>().FirstOrDefault();
            var response = await _webClient.CreateCustomer(req);
            response.Should().NotBeNull();
            _scenarioContext.Set<ApiResult>(response, "duplicated_customer_email_info_request_response");
        }

        [Then(@"customer email duplicated error message should be ""([^""]*)""")]
        public void ThenCustomerEmailDuplicatedErrorMessageShouldBe(string errorMessage)
        {
            var response = (ApiResult)_scenarioContext["duplicated_customer_email_info_request_response"];
            response.Should().NotBeNull();
            response.IsSuccess.Should().BeFalse();
            response.Message.Equals(errorMessage);
        }
        public byte ToNumber(string count)
        {
            return Convert.ToByte(count);
        }

        public async Task<List<CustomerOutputResponse>> GetAllCustomerAndFilterSpecifiedCustomerFromApis(string email)
        {
            var apiResult = await _webClient.GetAllCustomer();
            apiResult.Should().NotBeNull();
            apiResult.IsSuccess.Should().BeTrue();
            apiResult.Data.Should().NotBeNull();
            var specifiedCustomer = apiResult.Data.Where(p => p.Email.Equals(email)).ToList();
            return specifiedCustomer;
        }
    }
}