using Gems.AddressRegistry.OsmDataParser.Interfaces;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Parsers;

namespace Gems.AddressRegistry.OsmDataParser;

public static class OsmParserFactory
{
    public static IOsmParser<TResult> Create<TResult>()
    {
        if (typeof(TResult) == typeof(Area))
            return (new AreaParser() as IOsmParser<TResult>)!;
        
        if (typeof(TResult) == typeof(District))
            return (new DistrictParser() as IOsmParser<TResult>)!;
        
        if (typeof(TResult) == typeof(Settlement))
            return (new SettlementParser() as IOsmParser<TResult>)!;
        
        if (typeof(TResult) == typeof(City))
            return (new CityParser() as IOsmParser<TResult>)!;
        
        if (typeof(TResult) == typeof(Village))
            return (new VillageParser() as IOsmParser<TResult>)!;
        
        if (typeof(TResult) == typeof(AdminDistrict))
            return (new AdminDistrictParser() as IOsmParser<TResult>)!;
        
        if (typeof(TResult) == typeof(Street))
            return (new StreetParser() as IOsmParser<TResult>)!;
        
        if (typeof(TResult) == typeof(House))
            return (new HouseParser() as IOsmParser<TResult>)!;

        throw new NotSupportedException($"No parser for {typeof(TResult)}");
    }
}