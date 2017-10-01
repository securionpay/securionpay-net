using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Internal
{
    public class UnixDateConverter
    {
        DateTime _dtReferenceDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public long ToUnixTimeStamp(DateTime date)
        {
            var timespan = date - _dtReferenceDateTime;
            var unixFormat = (long)timespan.TotalSeconds;
            return unixFormat;
        }

        public DateTime FromUnixTimeStamp(long date)
        {
            var dtDateTime = _dtReferenceDateTime.AddSeconds(date);
            return dtDateTime;
        }
    }
}
