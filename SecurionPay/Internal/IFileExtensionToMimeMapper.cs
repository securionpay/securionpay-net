using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Internal
{
    public interface IFileExtensionToMimeMapper
    {
        string GetMimeType(string fileName);
    }
}
