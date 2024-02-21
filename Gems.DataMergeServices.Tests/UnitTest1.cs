using Gems.DataMergeServices.Services;

namespace Gems.DataMergeServices.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async void ConverterTest()
        {
            FiasXmlToEntityConverter fiasXmlToEntityConverter = new FiasXmlToEntityConverter();

            fiasXmlToEntityConverter.ConvertRegion("C:/Users/user/Desktop/AS_ADDR_OBJ_20231120_38a14e2c-9609-44e0-8688-edb6afc8cea1.XML");
            fiasXmlToEntityConverter.ConvertBuildings("C:/Users/user/Desktop/AS_HOUSES_20231120_191d6b01-474f-4c9e-946b-f8537e4103b8.XML");


        }
    }
}