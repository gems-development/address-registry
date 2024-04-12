using Gems.AddressRegistry.DataAccess;
using Gems.AddressRegistry.Entities;
using Gems.AddressRegistry.Entities.DataSources;
using Gems.ApplicationServices.Services;
using Gems.DataMergeServices.Services;

namespace Gems.DataMergeServices.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async void ConverterTest()
        {
            FiasXmlToEntityConverter fiasXmlToEntityConverter = new FiasXmlToEntityConverter();

            var reg =("C:\\Users\\user\\source\\repos\\address-registry\\test_data/addr_magadan.XML");
            var buildings = ("C:\\Users\\user\\source\\repos\\address-registry\\test_data/buildings_magadan.XML");
            var adm = ("C:\\Users\\user\\source\\repos\\address-registry\\test_data/adm_magadan.XML");
            var mun = ("C:\\Users\\user\\source\\repos\\address-registry\\test_data\\mun_magadan.XML");
            

            const string connectionString = "Host=localhost;Port=5432;Database=addressdb;Username=postgres;Password=admin";
            IAppDbContextFactory appDbContextFactory = new AppDbContextFactory(connectionString);
            DataImportService dataImportService = new DataImportService(appDbContextFactory);
            await dataImportService.ImportAddressesAsync(fiasXmlToEntityConverter.ReadAndBuildAddresses(adm, mun, buildings, reg).Result);

        }
    }
}