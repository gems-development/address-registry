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
            IAppDbContextFactory appDbContextFactory = new AppDbContextFactory();
            DataImportService dataImportService = new DataImportService(appDbContextFactory);
            Address address = new Address();
            address.Country = new Country();
            address.Country.Name = "Russia";
            address.Region = new Region();
            address.Region.Name = "Omskaya Oblast";
            address.Region.Code = "55";
            address.Settlement = new Settlement();
            address.Settlement.Name = "Orlovka";
            address.MunicipalArea = new MunicipalArea();
            address.MunicipalArea.Name = "Kalachinsky Rayon";
            address.Territory = new Territory();
            address.Territory.Name = "Terrytory";
            Address[]addresses= new Address[] { address };
            await dataImportService.AddressImportAsync(addresses);
            await dataImportService.AddressImportAsync(addresses);
            /* RoadNetworkElement roadNetworkElement1 = new RoadNetworkElement();
             roadNetworkElement1.Name = "Lenina";
             roadNetworkElement1.RoadNetworkElementType = Gems.AddressRegistry.Entities.Enums.RoadNetworkElementType.Street;
             RoadNetworkElement roadNetworkElement2 = new RoadNetworkElement();
             roadNetworkElement2.Name = "Lenina";
             roadNetworkElement2.RoadNetworkElementType = Gems.AddressRegistry.Entities.Enums.RoadNetworkElementType.Square;
             RoadNetworkElement[] roadNetworkElements = new RoadNetworkElement[2];
             roadNetworkElements[0] = roadNetworkElement1;
             roadNetworkElements[1] = roadNetworkElement2;
             await dataImportService.RoadNetworkElementImportAsync(roadNetworkElements);
             await dataImportService.RoadNetworkElementImportAsync(roadNetworkElements);*/
        }

    }
}