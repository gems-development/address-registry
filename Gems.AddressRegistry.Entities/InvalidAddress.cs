using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gems.AddressRegistry.Entities
{
    public class InvalidAddress : Address
    {
        public Address address;

        public InvalidAddress() { }

        public InvalidAddress(Address address) { this.address = address; }
    }
}
