using Mc2.CrudTest.AcceptanceTests.Drivers;
using Mc2.CrudTest.AcceptanceTests.Models;
using Mc2.CrudTest.AcceptanceTests.Models.DTOs.InputDtos;
using TechTalk.SpecFlow.Assist;

namespace Mc2.CrudTest.AcceptanceTests.Steps;

[Binding]
public class ManageCustomersSteps
{
    private readonly WebClientDriver _webClient;
    private readonly CustomerInputRequest _customerInputRequest;
    private readonly ScenarioContext _scenarioContext;
    private IEnumerable<BusinessError> _businessErrors = new List<BusinessError>();

    public ManageCustomersSteps(WebClientDriver webClient, ScenarioContext scenarioContext)
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

    [When(@"user create customer")]
    public async Task WhenUserCreateCustomer()
    {
        var response = await _webClient.CreateCustomer(_customerInputRequest);
        _scenarioContext.Add($"createdCustomer{response.Id}", response);
    }
}