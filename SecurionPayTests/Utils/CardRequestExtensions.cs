using SecurionPay.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.Utils
{
    public static class CardRequestExtensions
    {
        public static string GetLast4(this CardRequest request)
        {
            var number = request.Number;
            var numberLength = number.Length;
            return number.Substring(numberLength - 4);
        }
    }
}
