using Gems.AddressRegistry.DataAccess;
using Gems.AddressRegistry.Entities;
using Gems.ApplicationServices.Services;


namespace ApplicationServices.Tests
{
    public class DataAccessTest
    {

        [RunnableInDebugOnlyFactAttribute]
        public async Task ErnImportTest()
        {
            const string connectionString = "Host=localhost;Port=5432;Database=addressdb;Username=postgres;Password=admin";
            IAppDbContextFactory appDbContextFactory = new AppDbContextFactory(connectionString);
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