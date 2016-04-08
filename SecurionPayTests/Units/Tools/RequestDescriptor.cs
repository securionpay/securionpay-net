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
        public string Address { get; set; }
        public List<string> Header { get; set; }
        public string Content { get; set; }

        public bool Match(HttpRequestMessage message,string content)
        {
            var headersMatching = message.Headers.All(x=> Header.Contains(x.Key+": "+x.Value.First()) && Header.Count == message.Headers.Count());
            
            return Method == message.Method && Address==message.RequestUri.AbsoluteUri && headersMatching && Content== content;
        }
    }
}
