using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPay
{
    public interface IApiClient
    {
        Task<T> SendRequest<T>(HttpMethod method, string url, object parameter);
        Task<T> SendMultiPartRequest<T>(HttpMethod method, string url, Dictionary<string, string> form, byte[] fileBody, string fileName);
    }
}
