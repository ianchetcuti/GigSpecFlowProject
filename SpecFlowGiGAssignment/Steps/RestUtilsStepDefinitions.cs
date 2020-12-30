using FluentAssertions;
using GigSpecFlowProject;
using TechTalk.SpecFlow;
using System;
using Newtonsoft.Json.Linq;

namespace SpecFlowGiGAssignment.Steps
{
    [Binding]
    public sealed class RestfulApiStepDefinitions
    {
        private readonly RestUtils _restUtils = new RestUtils();
        private readonly ScenarioContext _scenarioContext;
        private RestRequest restRequest;
        private RestResponse restResponse = null;
        private JObject payloadBuilder = new JObject();
        
        public RestfulApiStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

        }
        [BeforeScenario]
        public void RunBeforeScenario()
        {
            Console.WriteLine("Initiating before method");
            restRequest = new RestRequest();
            restRequest.Payload = new Newtonsoft.Json.Linq.JObject();
            restResponse = new RestResponse();
        }

        [Given(@"the email is ""(.*)""")]
        public void GivenTheEmailIs(string p0)
        { 
            restRequest.Payload.Add("email", p0);
        }

        [Given(@"the password is ""(.*)""")]
        public void GivenThePasswordIs(string p0)
        {
            restRequest.Payload.Add("password",p0);
        }

        [When(@"the POST Request is sent to ""(.*)""")]
        public async System.Threading.Tasks.Task WhenThePOSTRequestIsSentToAsync(string p0)
        {
            restRequest.Path = p0;
            restRequest.Verb = "POST";
            restResponse = await _restUtils.SendRequestAsync(restRequest);
        }

        [Then(@"the response should be HTTP (.*) and a token")]
        public void ThenTheResponseShouldBeHTTPAndAToken(int p0)
        {
            //ScenarioContext.Current.Pending();
            restResponse.HTTPCode.Should().Be(p0);

            JObject assertObject = new JObject();
            assertObject.Add("id", 2);
            assertObject.Add("token", "QpwL5tke4Pnpja7X2");

            restResponse.Payload.Should().BeEquivalentTo(assertObject);
        }

        [Then(@"the response should be HTTP (.*) and an error")]
        public void ThenTheResponseShouldBeHTTPAndAnError(int p0)
        {
            restResponse.HTTPCode.Should().Be(p0);

            JObject assertObject = new JObject();
            assertObject.Add("error", "Missing password");

            restResponse.Payload.Should().BeEquivalentTo(assertObject);
        }

        [When(@"the GET Request is sent to ""(.*)""")]
        public async System.Threading.Tasks.Task WhenTheGETRequestIsSentToAsync(string p0)
        {
            restRequest.Path = p0;
            restRequest.Verb = "GET";
            restResponse = await _restUtils.SendRequestAsync(restRequest);
        }

        [Then(@"the response should be HTTP (.*) and a list of users")]
        public void ThenTheResponseShouldBeHTTPAndAListOfUsers(int p0)
        {
            restResponse.HTTPCode.Should().Be(p0);

            var valueArray = (JArray)restResponse.Payload["data"];

            //there is at least 1 user in the response payload under the data JSON key
            valueArray.Should().HaveCountGreaterOrEqualTo(1);
        }


    }
}
