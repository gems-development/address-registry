using ApplicationServices.Services;
using Gems.AddressRegistry.DataAccess;
using Gems.AddressRegistry.Entities;

namespace ApplicationServicesTests
{
    public class UnitTest1
    {
        [Fact]
        public async Task ErnImportTest()
        {
            AppDbContext appDbContext = new AppDbContext();
            DataImportService dataImportService = new DataImportService(appDbContext);
            RoadNetworkElement roadNetworkElement1 = new RoadNetworkElement();
            roadNetworkElement1.Name = "Lenina";
            roadNetworkElement1.RoadNetworkElementType = Gems.AddressRegistry.Entities.Enums.RoadNetworkElementType.Street;
            RoadNetworkElement roadNetworkElement2 = new RoadNetworkElement();
            roadNetworkElement2.Name = "Lenina";
            roadNetworkElement2.RoadNetworkElementType = Gems.AddressRegistry.Entities.Enums.RoadNetworkElementType.Square;
            RoadNetworkElement[] roadNetworkElements = new RoadNetworkElement[2];
            roadNetworkElements[0] = roadNetworkElement1;
            roadNetworkElements[1] = roadNetworkElement2;
            await dataImportService.RoadNetworkElementImportAsync(roadNetworkElements);
            await dataImportService.RoadNetworkElementImportAsync(roadNetworkElements);
        }

    }
}