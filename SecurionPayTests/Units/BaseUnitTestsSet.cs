using Xunit;
using Newtonsoft.Json;
using SecurionPayTests.Units.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.Units
{
    public class BaseUnitTestsSet
    {
        protected string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        protected RequestTester GetRequestTester()
        {
            return new RequestTester();
        }
    }
}
