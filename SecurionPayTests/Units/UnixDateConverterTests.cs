using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurionPay.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.Units
{
    [TestClass]
    public class UnixDateConverterTests : BaseUnitTestsSet
    {
        UnixDateConverter _subject = new UnixDateConverter();

        [TestMethod]
        public void ToUnixTimeStampTest()
        {
            var result = _subject.ToUnixTimeStamp(new DateTime(2017, 10, 01, 12, 33, 37, DateTimeKind.Utc));
            Assert.AreEqual(1506861217, result);
        }

        [TestMethod]
        public void FromUnixTimeStampTest()
        {
            var result = _subject.FromUnixTimeStamp(1506861217);
            Assert.AreEqual(new DateTime(2017,10,01,12,33,37,DateTimeKind.Utc), result);
        }

    }
}
