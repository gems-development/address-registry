using Gems.DataMergeServices.Services;
using System.Diagnostics;

namespace Gems.DataMergeServices.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async void ConverterTest()
        {
            FiasXmlToEntityConverter fiasXmlToEntityConverter = new FiasXmlToEntityConverter();

            await fiasXmlToEntityConverter.ConvertRegion("C:/Users/user/Desktop/AS_ADDR_OBJ_20231120_38a14e2c-9609-44e0-8688-edb6afc8cea1.XML");
             
        }
    }
}