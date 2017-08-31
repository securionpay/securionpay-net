using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.Units.Tools
{
    public class RequestDescriptor
    {
        public HttpMethod Method { get; set; }

        public string Action { get; set; }

        public object Parameter { get; set; }
    }
}
