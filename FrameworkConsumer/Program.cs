using System;
using System.Threading;
using GigSpecFlowProject;

namespace FrameworkConsumer
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            RestUtils restUtils = new RestUtils();

            RestRequest r = new RestRequest();

            r.Verb = "POST";
            r.Path = "https://reqres.in/api/register";

            r.Payload = new Newtonsoft.Json.Linq.JObject();
            r.Payload.Add("email", "janet.weaver@reqres.in");
            r.Payload.Add("password", "weakPass");

            RestResponse restResponse = await restUtils.SendRequestAsync(r);

            Console.WriteLine(restResponse.Payload.ToString());

            RestRequest r2 = new RestRequest();

            r2.Verb = "GET";
            r2.Path = "https://reqres.in/api/users";

            RestResponse restResponse2 = await restUtils.SendRequestAsync(r2);
            Console.WriteLine(restResponse2.Payload.ToString());

            await publishMessageAsync();

            consumeMessages();
        }

        private static StreamUtils streamUtils = new StreamUtils();

        static async System.Threading.Tasks.Task publishMessageAsync()
        {
            Console.WriteLine(await streamUtils.publishKafkaMessageAsync("localhost:9092","car","testMessage"));
        }

        static void consumeMessages()
        {
            foreach (String s in streamUtils.consumeKafkaMessages("localhost:9092","car"))
            {
               Console.WriteLine(s);
            }
        }
    }
}
