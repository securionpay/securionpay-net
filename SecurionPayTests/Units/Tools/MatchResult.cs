using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.Units.Tools
{
    public class MatchResult
    {
        public MatchResult()
        {
            MatchFailReasonsMessage = string.Empty;
        }
        public bool MatchSuccess { get; set; }
        public string MatchFailReasonsMessage { get; set; }
    }
}
