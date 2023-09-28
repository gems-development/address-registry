using Gems.AddressRegistry.DataAccess;
using Gems.AddressRegistry.Entities;

using (AddressContext db = new AddressContext())
{
    Address address = new Address();
    db.Add(address);
}