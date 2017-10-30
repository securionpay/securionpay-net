using SecurionPay.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.ModelBuilders
{
    public class AddressBuilder : IBuilder<Address>
    {

        public Address Build()
        {
            return new Address()
            {
                City="SomeCity",
                CountryISOCode = "CH",
                FirstLine="some street",
                SecondLine = "some street second line",
                State = "Bern",
                ZipCode = "3010"
            };
        }
    }
}
