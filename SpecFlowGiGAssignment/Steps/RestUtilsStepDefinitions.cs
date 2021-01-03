using FluentAssertions;
using GigSpecFlowProject;
using TechTalk.SpecFlow;
using System;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SpecFlowGiGAssignment.Steps
{
    [Binding]
    public sealed class RestfulApiStepDefinitions
    {
        private readonly RestUtils _restUtils = new RestUtils();
        private RestRequest restRequest;
        private RestResponse restResponse = null;

        // Gets configuration from AppSettings.json file
        private IConfiguration config = new ConfigurationBuilder()
          .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).ToString() + "\\SpecFlowGiGAssignment\\Configuration")
          .AddJsonFile("AppSettings.json", false, true)
          .Build();

        [BeforeScenario]
        public void RunBeforeScenario()
        {
            Console.WriteLine("Initiating before method");
            restRequest = new RestRequest();
            restRequest.Payload = new JObject();
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
            restResponse.HTTPCode.Should().Be(p0);

            JObject assertObject = new JObject();
            assertObject.Add("id", config["SpecFlowTestData:RegistrationTests:ReturnId"]);
            assertObject.Add("token", config["SpecFlowTestData:RegistrationTests:TokenCode"]);

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
