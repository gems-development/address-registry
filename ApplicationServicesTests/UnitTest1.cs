using ApplicationServices.Services;
using Gems.AddressRegistry.DataAccess;
using Gems.AddressRegistry.Entities;

namespace ApplicationServices.Tests
{
    public class UnitTest1
    {
        [RunnableInDebugOnlyFactAttribute]
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
            Address[] addresses = new Address[] { address };
            await dataImportService.AddressImportAsync(addresses);
            await dataImportService.AddressImportAsync(addresses);

        }

    }
}