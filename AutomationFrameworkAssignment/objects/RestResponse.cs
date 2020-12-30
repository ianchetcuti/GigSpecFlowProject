using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace GigSpecFlowProject
{
    public class RestResponse
    {
        public int HTTPCode { get; set; }
        public JObject Payload { get; set; }
    }
}
