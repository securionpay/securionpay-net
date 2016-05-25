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

        public MatchResult Match(HttpRequestMessage message,string content)
        {
            var headersMatching = message.Headers.All(x=> Header.Contains(x.Key+": "+x.Value.First()) && Header.Count == message.Headers.Count());
            MatchResult result = new MatchResult();
            result.MatchSuccess = true;
            if(Method != message.Method)
            {
                result.MatchSuccess = false;
                result.MatchFailReasonsMessage += "Wrong http method. ";
            }
            if (Address != message.RequestUri.AbsoluteUri)
            {
                result.MatchSuccess = false;
                result.MatchFailReasonsMessage += "Wrong address. ";
            }
            if (!headersMatching)
            {
                result.MatchSuccess = false;
                result.MatchFailReasonsMessage += "Wrong header. ";
            }
            if(Content != content)
            {
                result.MatchSuccess = false;
                result.MatchFailReasonsMessage += "Wrong content. ";
            }
            return result;
        }
    }
}
