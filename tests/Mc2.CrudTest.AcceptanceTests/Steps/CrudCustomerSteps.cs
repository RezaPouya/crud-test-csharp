using System.Net;
using Mc2.CrudTest.AcceptanceTests.Drivers;
using Mc2.CrudTest.AcceptanceTests.Models;
using Mc2.CrudTest.AcceptanceTests.Models.DTOs.InputDtos;
using TechTalk.SpecFlow.Assist;

namespace Mc2.CrudTest.AcceptanceTests.Steps;

[Binding]
public class CrudCustomerSteps
{
    private readonly WebClientDriver _webClient;
    private readonly CustomerInputRequest _customerInputRequest;
    private readonly ScenarioContext _scenarioContext;
    private IEnumerable<BusinessError> _businessErrors = new List<BusinessError>();
    
    public CrudCustomerSteps(WebClientDriver webClient, ScenarioContext scenarioContext)
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
}