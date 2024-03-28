
using Gems.AddressRegistry.Entities;

namespace Gems.ApplicationServices.Services
{
    internal static class DataImportHelper
    {
        public static void Map (Building source,  Building target)
        {
            target.Number = source.Number;
        }
    }
}
