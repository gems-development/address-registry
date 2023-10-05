using Gems.AddressRegistry.DataAccess;
using Gems.AddressRegistry.Entities;

namespace Gems.AddressRegistry.ApplicationServices
{
    public class DataImportService
    {
        AddressContext AddressContext = new AddressContext();
        public DataImportService() { }

        public void ERNImport(ERN[] ERN)
        {
            foreach(ERN eRNs in ERN) 
            {
                if (AddressContext.Addresses.Any(ERN => ERN.Id == eRNs.Id))
                    AddressContext.Update(eRNs);
                else
                AddressContext.Add(eRNs); 
            }
            AddressContext.SaveChanges();
        }

        
    }
}