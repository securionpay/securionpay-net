using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SecurionPay.Internal
{
    public class SignService : ISignService
    {
        IConfigurationProvider _configurationProvider;

        public SignService(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public string Sign(string data)
        {
            var hash = new HMACSHA256(Encoding.UTF8.GetBytes(_configurationProvider.GetSecretKey()));
            var hashedData = hash.ComputeHash(Encoding.UTF8.GetBytes(data));
            return BitConverter.ToString(hashedData).Replace("-", string.Empty).ToLower();
        }
    }
}
