using Gems.AddressRegistry.DataAccess;
using Gems.AddressRegistry.Entities;

namespace Gems.AddressRegistry.ApplicationServices
{
    public class DataImportService
    {
        private AddressContext _addressContext = new AddressContext();
        public DataImportService() { }

        public void ErnImport(RoadNetworkElement[] Ern)
        {
            foreach(RoadNetworkElement eRNs in Ern) 
            {
                if (_addressContext.Addresses.Any(Ern => Ern.Id == eRNs.Id))
                    _addressContext.Update(eRNs);
                else
                _addressContext.Add(eRNs); 
            }
            _addressContext.SaveChanges();
        }

        
    }
}