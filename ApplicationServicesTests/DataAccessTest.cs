using Gems.AddressRegistry.DataAccess;
using Gems.AddressRegistry.Entities;
using Gems.ApplicationServices.Services;
using Microsoft.Extensions.Logging;
using Moq;


namespace ApplicationServices.Tests
{
    public class DataAccessTest
    {

        private readonly ILogger _logger = new Mock<ILogger>().Object;

        [RunnableInDebugOnlyFactAttribute]
        public async Task ErnImportTest()
        {
            const string connectionString = "Host=localhost;Port=5432;Database=addressdb;Username=postgres;Password=postgres";
            IAppDbContextFactory appDbContextFactory = new AppDbContextFactory(connectionString);
            DataImportService dataImportService = new DataImportService(appDbContextFactory, _logger);
            Address address = new Address();
            
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
            await dataImportService.ImportAddressesAsync(addresses, _logger);
            await dataImportService.ImportAddressesAsync(addresses, _logger);

        }

    }
}