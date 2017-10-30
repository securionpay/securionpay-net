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
            return GetLast4(request.Number);
        }

        public static string GetLast4(this TokenRequest request)
        {
            return GetLast4(request.Number);
        }

        private static string GetLast4(string number)
        {
            var numberLength = number.Length;
            return number.Substring(numberLength - 4);
        }
    }
}
