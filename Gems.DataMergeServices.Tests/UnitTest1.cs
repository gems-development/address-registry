using Gems.AddressRegistry.DataAccess;
using Gems.AddressRegistry.Entities;
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

            fiasXmlToEntityConverter.ConvertRegion("C:/Users/user/Desktop/test/region_test.XML");
            fiasXmlToEntityConverter.ConvertBuildings("C:/Users/user/Desktop/test/buildings_test.XML");
            fiasXmlToEntityConverter.ReadAdmHierarchy("C:/Users/user/Desktop/test/adm_test.XML");
            await fiasXmlToEntityConverter.ReadMunHierarchy("C:/Users/user/Desktop/test/mun_test.XML");
            

            const string connectionString = "Host=localhost;Port=5432;Database=addressdb;Username=postgres;Password=admin";
            IAppDbContextFactory appDbContextFactory = new AppDbContextFactory(connectionString);
            DataImportService dataImportService = new DataImportService(appDbContextFactory);
            await dataImportService.AddressImportAsync(fiasXmlToEntityConverter.BuildAddresses());

        }
    }
}