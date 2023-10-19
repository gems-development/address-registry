using Gems.AddressRegistry.DataAccess;
using Gems.AddressRegistry.Entities;

namespace Gems.AddressRegistry.ApplicationServices
{
    public class DataImportService
    {
        private AddressContext _addressContext = new AddressContext();
        public DataImportService() { }

        public void RoadNetworkElementImport(IReadOnlyCollection<RoadNetworkElement> Rne)
        {
            foreach(RoadNetworkElement rNEs in Rne) 
            {
                if (_addressContext.RoadNetworkElements.Any(RoadNetworkElement => RoadNetworkElement.Id == rNEs.Id))
                    _addressContext.Update(rNEs);
                else
                _addressContext.Add(rNEs); 
            }
            _addressContext.SaveChanges();
        }

        
    }
}