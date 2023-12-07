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

            await fiasXmlToEntityConverter.ConvertRegion("C:/Users/user/Desktop/AS_ADDR_OBJ_20231120_0a177ef4-4611-43ce-b9ab-c03b5b266d9a.XML");
            
        }
    }
}