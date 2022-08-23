using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoDi;
using Mc2.CrudTest.AcceptanceTests.Drivers;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using TechTalk.SpecFlow;

namespace Mc2.CrudTest.AcceptanceTests.Hooks;

[Binding]
public sealed class CreateReadEditDeleteCustomerHooks
{
    // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
    private readonly IObjectContainer _objectContainer;

    public CreateReadEditDeleteCustomerHooks(IObjectContainer objectContainer)
    {
        _objectContainer = objectContainer;
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        var config = LoadConfiguration();
        var baseUrl = config["Customer.Api:BaseAddress"];
        var httpClient = new HttpClient()
        {
            BaseAddress = new Uri(baseUrl)
        };

        _objectContainer.RegisterInstanceAs(httpClient);

        var webClientDriver = new WebClientDriver(httpClient);

        _objectContainer.RegisterInstanceAs(webClientDriver);
    }

    [AfterScenario]
    public void AfterScenario()
    {
        //TODO: implement logic that has to run after executing each scenario
    }


    private static IConfiguration LoadConfiguration()
    {
        return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    }
}