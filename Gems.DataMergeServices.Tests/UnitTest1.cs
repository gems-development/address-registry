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

            fiasXmlToEntityConverter.ConvertRegion("C:\\Users\\kds04\\source\\repos\\address-registry\\test_data/region_test.XML");
            fiasXmlToEntityConverter.ConvertBuildings("C:\\Users\\kds04\\source\\repos\\address-registry\\test_data/buildings_test.XML");
            fiasXmlToEntityConverter.ReadAdmHierarchy("C:\\Users\\kds04\\source\\repos\\address-registry\\test_data/adm_test.XML");
            await fiasXmlToEntityConverter.ReadMunHierarchy("C:\\Users\\kds04\\source\\repos\\address-registry\\test_data/mun_test.XML");
            

            const string connectionString = "Host=localhost;Port=5432;Database=addressdb;Username=postgres;Password=postgres";
            IAppDbContextFactory appDbContextFactory = new AppDbContextFactory(connectionString);
            DataImportService dataImportService = new DataImportService(appDbContextFactory);
            

        }
    }
}