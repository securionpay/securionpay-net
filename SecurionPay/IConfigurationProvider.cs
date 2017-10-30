using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay
{
    public interface IConfigurationProvider
    {
        string GetApiUrl();
        string GetUploadsUrl();
    }
}
