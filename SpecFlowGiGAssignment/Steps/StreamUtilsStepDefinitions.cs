using FluentAssertions;
using GigSpecFlowProject;
using TechTalk.SpecFlow;
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace SpecFlowGiGAssignment.Steps
{
    [Binding]
    public sealed class StreamUtilsStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        private readonly StreamUtils _streamUtils = new StreamUtils();
        private List<Car> carList;
        
        public StreamUtilsStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

        }
        [BeforeScenario]
        public void RunBeforeScenario()
        {
            Console.WriteLine("Initiating before method");

            carList = new List<Car>();
        }

        [Given(@"the Brand is a ""(.*)"" and the model is a ""(.*)"" and the number of doors are (.*) and it ""(.*)"" a sports car")]
        public void GivenTheBrandIsAndTheModelIsAndTheNumberOfDoorsAreAndItASportsCar(string p0, string p1, int p2, string p3)
        {
            Car car = new Car();
            car.BrandName = p0;
            car.Model = p1;
            car.NumberOfDoors = p2;

            if (p3.Contains("isnt"))
                car.IsSportsCar = false;
            else if (p3.Contains("is"))
                car.IsSportsCar = true;
            else
                throw new Exception("Unrecognised parameter in Gherkin statement");
            carList.Add(car);
        }

        [When(@"the car messages are published")]
        public async System.Threading.Tasks.Task WhenTheCarMessagesArePublishedAsync()
        {
            JObject jObjectCar;
            foreach(Car car in carList)
            {
                jObjectCar = new JObject();

                jObjectCar.Add("brandName",car.BrandName);
                jObjectCar.Add("modelName", car.Model);
                jObjectCar.Add("numberOfDoors", car.NumberOfDoors);
                jObjectCar.Add("isASportsCar", car.IsSportsCar);

                _ = await _streamUtils.publishKafkaMessageAsync("localhost:9092", "test", jObjectCar.ToString(Newtonsoft.Json.Formatting.None));
            }
        }

        [Then(@"the total consumed messages are (.*)")]
        public void ThenTheTotalConsumedMessagesAre(int p0)
        {
            carList.Should().HaveCount(p0);
        }
    }
}
