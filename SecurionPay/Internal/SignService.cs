using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SecurionPay.Internal
{
    public class SignService : ISignService
    {
        ISecretKeyProvider _secretKeyProvider;

        public SignService(ISecretKeyProvider secretKeyProvider)
        {
            _secretKeyProvider = secretKeyProvider;
        }

        public string Sign(string data)
        {
            var hash = new HMACSHA256(Encoding.UTF8.GetBytes(_secretKeyProvider.GetSecretKey()));
            var hashedData = hash.ComputeHash(Encoding.UTF8.GetBytes(data));
            return BitConverter.ToString(hashedData).Replace("-", string.Empty).ToLower();
        }
    }
}
