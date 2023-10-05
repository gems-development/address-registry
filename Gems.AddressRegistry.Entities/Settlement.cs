using Gems.AddressRegistry.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Gems.AddressRegistry.Entities
{
    public class Settlement : BaseAuditableEntity
    {
        public string Name
        {
            get; set;
        }
    }
}
