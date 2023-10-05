using Gems.AddressRegistry.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gems.AddressRegistry.Entities
{
    public class Space : BaseAuditableEntity
    {
        public int Number { get; set; }

        public enum Type { Room, Office, etc }
    }
}
