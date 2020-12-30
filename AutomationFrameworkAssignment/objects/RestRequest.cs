using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace GigSpecFlowProject
{
    public class RestRequest
    {
        public String Verb { set; get; }
        public String Path { set; get; }
        public JObject Payload { set; get; }
    }
}
