using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GigSpecFlowProject
{
    public class RestUtils
    {
        private static HttpClient client = new HttpClient();
        private RestResponse restResponse;
        
        public async System.Threading.Tasks.Task<RestResponse> SendRequestAsync(RestRequest restRequest)
        {
            HttpResponseMessage response = null;

            if (restRequest.Verb == "POST" && restRequest.Payload != null)
            {
                Console.WriteLine(restRequest.Payload);

                var httpContent = new StringContent(restRequest.Payload.ToString());
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");


                response = await client.PostAsync(restRequest.Path, httpContent);
            } 
            else if (restRequest.Verb == "GET")
            {
                response = await client.GetAsync(restRequest.Path);
            }
 
            if (response!= null)
            {
                restResponse = new RestResponse();

                restResponse.HTTPCode = (int)response.StatusCode;
                restResponse.Payload = JObject.Parse(await response.Content.ReadAsStringAsync());
            }
            else { throw new Exception("Unsupported operation requested"); }

            return restResponse;
        }
    }
}
