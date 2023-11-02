using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gems.AddressRegistry.DataAccess
{
    public class AppDbContextFactory : IAppDbContextFactory
    {
        public IAppDbContext Create() 
        { 
            return new AppDbContext();
        }
    }
}
