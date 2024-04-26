using Gems.AddressRegistry.DataAccess;
using Gems.AddressRegistry.Entities;
using MediatR;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using WebApi.Dto.Response;
using WebApi.MediatrRequests;

namespace WebApi.Handlers;

public class GetAddressByLocationHandler : IRequestHandler<GetAddressByLocationRequest, AddressDtoResponse>
{
    private readonly IAppDbContext _context;
    private const float BufferRadius = 0.00009F; // 10м
    private static readonly GeoJsonReader Reader = new GeoJsonReader();
    
    public GetAddressByLocationHandler(IAppDbContextFactory appDbContextFactory)
    {
        _context = appDbContextFactory.Create();
    }
    
    public async Task<AddressDtoResponse> Handle(GetAddressByLocationRequest request, CancellationToken cancellationToken)
    {
        var lat = request.GetAddressByLocationDto.Lat;
        var lon = request.GetAddressByLocationDto.Long;
        
        var sortPoint  = new Point(lat,lon);
        var buffer = sortPoint.Buffer(BufferRadius, 8);
        
        var cities = _context.Cities.ToList();
        var settlements = _context.Settlements.ToList();
        var roadNetworkElements = _context.RoadNetworkElements.ToList();
        var planingStructureElements = _context.PlaningStructureElements.ToList();
        var buildings = _context.Buildings.ToList();
        var address = new Address();

        foreach (var city in cities)
        {
            var cityGeometry = Reader.Read<FeatureCollection>(city.GeoJson);
            var cityFeature = cityGeometry.FirstOrDefault();
            
            if (buffer.Intersects(cityFeature!.Geometry))
            {
                address.City = city;
                var rneInCity = roadNetworkElements.Where(street => street.City == city).ToList();
                var pseInCity = planingStructureElements.Where(street => street.City == city).ToList();
                var villagesInCity = settlements.Where(village => village.City == city).ToList();

                AddLowerElementsToAddress(buffer, address, buildings, rneInCity, pseInCity);

                if (villagesInCity.Any())
                {
                    foreach (var village in villagesInCity)
                    {
                        var villageGeometry = Reader.Read<FeatureCollection>(village.GeoJson);
                        var villageFeature = villageGeometry.FirstOrDefault();
                        if (buffer.Intersects(villageFeature!.Geometry))
                        {
                            address.Settlement = village;
                        }
                    }
                }
            }
            if (city.Territory is not null)
            {
                var territoryGeometry = Reader.Read<FeatureCollection>(city.Territory?.GeoJson);
                var territoryFeature = territoryGeometry.FirstOrDefault();
                if (buffer.Intersects(territoryFeature!.Geometry))
                {
                    address.Territory = city.Territory;
                    address.Region = city.Territory!.MunicipalArea!.Region;
                }
            }
            if (city.MunicipalArea is not null)
            {
                var munAreaGeometry = Reader.Read<FeatureCollection>(city.MunicipalArea?.GeoJson);
                var munAreaFeature = munAreaGeometry.FirstOrDefault();
                if (buffer.Intersects(munAreaFeature!.Geometry))
                {
                    address.MunicipalArea = city.MunicipalArea;
                    address.Region = city.MunicipalArea!.Region;
                }
            }
            if (city.AdministrativeArea is not null)
            {
                var admAreaGeometry = Reader.Read<FeatureCollection>(city.AdministrativeArea?.GeoJson);
                var admAreaFeature = admAreaGeometry.FirstOrDefault();
                if (buffer.Intersects(admAreaFeature!.Geometry))
                {
                    address.AdministrativeArea = city.AdministrativeArea;
                    address.Region = city.AdministrativeArea!.Region;
                }
            }
        }

        foreach (var settlement in settlements)
        {
            var settlementGeometry = Reader.Read<FeatureCollection>(settlement.GeoJson);
            var settlementFeature = settlementGeometry.FirstOrDefault();
            if (buffer.Intersects(settlementFeature!.Geometry))
            {
                address.Settlement = settlement;
                var rneInSettlement = roadNetworkElements
                    .Where(street => street.Settlement == settlement).ToList();
                var pseInSettlement = planingStructureElements
                    .Where(street => street.Settlement == settlement).ToList();
                
                AddLowerElementsToAddress(buffer, address, buildings, rneInSettlement, pseInSettlement);
            }
            if (settlement.Territory is not null)
            {
                var territoryGeometry = Reader.Read<FeatureCollection>(settlement.Territory?.GeoJson);
                var territoryFeature = territoryGeometry.FirstOrDefault();
                if (buffer.Intersects(territoryFeature!.Geometry))
                {
                    address.Territory = settlement.Territory;
                    address.Region = settlement.Territory!.MunicipalArea!.Region;
                }
            }
            if (settlement.MunicipalArea is not null)
            {
                var munAreaGeometry = Reader.Read<FeatureCollection>(settlement.MunicipalArea?.GeoJson);
                var munAreaFeature = munAreaGeometry.FirstOrDefault();
                if (buffer.Intersects(munAreaFeature!.Geometry))
                {
                    address.MunicipalArea = settlement.MunicipalArea;
                    address.Region = settlement.MunicipalArea!.Region;
                }
            }
        }
        
        var addressResponseDto = new AddressDtoResponse(address);
        return addressResponseDto;
    }

    private static void AddLowerElementsToAddress(
        Geometry buffer,
        Address address,
        List<Building> buildings,
        List<RoadNetworkElement> rneList, 
        List<PlaningStructureElement> pseList)
    {
        if (rneList.Any())
        {
            foreach (var rne in rneList)
            {
                var rneGeometry = Reader.Read<FeatureCollection>(rne.GeoJson);
                var rneFeature = rneGeometry.FirstOrDefault();
                if (buffer.Intersects(rneFeature!.Geometry))
                {
                    address.RoadNetworkElement = rne;
                    var buildingsOnRne = buildings.Where(building => building.RoadNetworkElement == rne).ToList();

                    foreach (var building in buildingsOnRne)
                    {
                        var buildingGeometry = Reader.Read<FeatureCollection>(building.GeoJson);
                        var buildingFeature = buildingGeometry.FirstOrDefault();
                        if (buffer.Intersects(buildingFeature!.Geometry))
                            address.Building = building;
                    }
                }
            }
        }

        if (pseList.Any())
        {
            foreach (var pse in pseList)
            {
                var pseGeometry = Reader.Read<FeatureCollection>(pse.GeoJson);
                var pseFeature = pseGeometry.FirstOrDefault();
                if (buffer.Intersects(pseFeature!.Geometry))
                {
                    address.PlaningStructureElement = pse;
                    var buildingsOnPse = buildings.Where(building => building.PlaningStructureElement == pse).ToList();

                    foreach (var building in buildingsOnPse)
                    {
                        var buildingGeometry = Reader.Read<FeatureCollection>(building.GeoJson);
                        var buildingFeature = buildingGeometry.FirstOrDefault();
                        if (buffer.Intersects(buildingFeature!.Geometry))
                            address.Building = building;
                    }
                }
            }
        }
    }
}