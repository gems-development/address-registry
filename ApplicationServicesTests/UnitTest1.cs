using Gems.AddressRegistry.ApplicationServices;
using Gems.AddressRegistry.Entities;

namespace ApplicationServicesTests
{
    public class UnitTest1
    {
        [Fact]
        public void ErnImportTest()
        {
            DataImportService dataImportService = new DataImportService();
            RoadNetworkElement roadNetworkElement1 = new RoadNetworkElement();
            roadNetworkElement1.Name = "Lenina";
            roadNetworkElement1.RoadNetworkElementType = Gems.AddressRegistry.Entities.Enums.RoadNetworkElementType.Street;
            RoadNetworkElement roadNetworkElement2 = new RoadNetworkElement();
            roadNetworkElement2.Name = "Lenina";
            roadNetworkElement2.RoadNetworkElementType = Gems.AddressRegistry.Entities.Enums.RoadNetworkElementType.Square;
            RoadNetworkElement[] roadNetworkElements = new RoadNetworkElement[2] ;
            roadNetworkElements[0] = roadNetworkElement1;
            roadNetworkElements[1] = roadNetworkElement2;
            dataImportService.ErnImport(roadNetworkElements);
//            dataImportService.ErnImport(roadNetworkElements);
        }
    
    }
}