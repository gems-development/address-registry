using Gems.AddressRegistry.DataAccess;
using Gems.AddressRegistry.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using WebApi.Dto;

namespace WebApi.UseCases.GetAddressByLocation;

public class GetAddressByLocationRequestHandler : IRequestHandler<GetAddressByLocationRequest, AddressDtoResponse>
{
    private readonly IAppDbContext _context;
    private const float BufferRadius = 0.00009F; // 10м
    private static readonly GeoJsonReader Reader = new GeoJsonReader();
    
    public GetAddressByLocationRequestHandler(IAppDbContextFactory appDbContextFactory)
    {
        _context = appDbContextFactory.Create();
    }
    
    public async Task<AddressDtoResponse> Handle(GetAddressByLocationRequest request, CancellationToken cancellationToken)
    {
        var lat = request.GetAddressByLocationDto.Lat;
        var lon = request.GetAddressByLocationDto.Long;
        
        var sortPoint  = new Point(lat,lon);
        var buffer = sortPoint.Buffer(BufferRadius, 8);
        
        var cities = await _context.Cities.ToArrayAsync(cancellationToken);
        var settlements = await _context.Settlements.ToArrayAsync(cancellationToken);
        var roadNetworkElements = await _context.RoadNetworkElements.ToArrayAsync(cancellationToken);
        var planingStructureElements = await _context.PlaningStructureElements.ToArrayAsync(cancellationToken);
        var buildings = await _context.Buildings.ToArrayAsync(cancellationToken);
        var address = new Address();

        foreach (var city in cities)
        {
            var cityGeometry = Reader.Read<FeatureCollection>(city.GeoJson);
            var cityFeature = cityGeometry.First();
            
            if (buffer.Intersects(cityFeature.Geometry))
            {
                address.City = city;
                var rneInCity = roadNetworkElements.Where(street => street.City == city).ToArray();
                var pseInCity = planingStructureElements.Where(street => street.City == city).ToArray();
                var villagesInCity = settlements.Where(village => village.City == city).ToArray();

                AddLowerElementsToAddress(buffer, address, buildings, rneInCity, pseInCity);

                if (villagesInCity.Any())
                {
                    foreach (var village in villagesInCity)
                    {
                        var villageGeometry = Reader.Read<FeatureCollection>(village.GeoJson);
                        var villageFeature = villageGeometry.First();
                        if (buffer.Intersects(villageFeature.Geometry))
                        {
                            address.Settlement = village;
                        }
                    }
                }
            }
            if (city.Territory is not null)
            {
                var territoryGeometry = Reader.Read<FeatureCollection>(city.Territory?.GeoJson);
                var territoryFeature = territoryGeometry.First();
                if (buffer.Intersects(territoryFeature.Geometry))
                {
                    address.Territory = city.Territory;
                    address.Region = city.Territory!.MunicipalArea!.Region;
                }
            }
            if (city.MunicipalArea is not null)
            {
                var munAreaGeometry = Reader.Read<FeatureCollection>(city.MunicipalArea?.GeoJson);
                var munAreaFeature = munAreaGeometry.First();
                if (buffer.Intersects(munAreaFeature.Geometry))
                {
                    address.MunicipalArea = city.MunicipalArea;
                    address.Region = city.MunicipalArea!.Region;
                }
            }
            if (city.AdministrativeArea is not null)
            {
                var admAreaGeometry = Reader.Read<FeatureCollection>(city.AdministrativeArea?.GeoJson);
                var admAreaFeature = admAreaGeometry.First();
                if (buffer.Intersects(admAreaFeature.Geometry))
                {
                    address.AdministrativeArea = city.AdministrativeArea;
                    address.Region = city.AdministrativeArea!.Region;
                }
            }
        }

        foreach (var settlement in settlements)
        {
            var settlementGeometry = Reader.Read<FeatureCollection>(settlement.GeoJson);
            var settlementFeature = settlementGeometry.First();
            if (buffer.Intersects(settlementFeature.Geometry))
            {
                address.Settlement = settlement;
                var rneInSettlement = roadNetworkElements
                    .Where(street => street.Settlement == settlement).ToArray();
                var pseInSettlement = planingStructureElements
                    .Where(street => street.Settlement == settlement).ToArray();
                
                AddLowerElementsToAddress(buffer, address, buildings, rneInSettlement, pseInSettlement);
            }
            if (settlement.Territory is not null)
            {
                var territoryGeometry = Reader.Read<FeatureCollection>(settlement.Territory?.GeoJson);
                var territoryFeature = territoryGeometry.First();
                if (buffer.Intersects(territoryFeature.Geometry))
                {
                    address.Territory = settlement.Territory;
                    address.Region = settlement.Territory!.MunicipalArea!.Region;
                }
            }
            if (settlement.MunicipalArea is not null)
            {
                var munAreaGeometry = Reader.Read<FeatureCollection>(settlement.MunicipalArea?.GeoJson);
                var munAreaFeature = munAreaGeometry.First();
                if (buffer.Intersects(munAreaFeature.Geometry))
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
        Building[] buildings,
        RoadNetworkElement[] rneArray, 
        PlaningStructureElement[] pseArray)
    {
        if (rneArray.Any())
        {
            foreach (var rne in rneArray)
            {
                var rneGeometry = Reader.Read<FeatureCollection>(rne.GeoJson);
                var rneFeature = rneGeometry.FirstOrDefault();
                if (buffer.Intersects(rneFeature!.Geometry))
                {
                    address.RoadNetworkElement = rne;
                    var buildingsOnRne = buildings.Where(building => building.RoadNetworkElement == rne).ToArray();

                    foreach (var building in buildingsOnRne)
                    {
                        var buildingGeometry = Reader.Read<FeatureCollection>(building.GeoJson);
                        var buildingFeature = buildingGeometry.First();
                        if (buffer.Intersects(buildingFeature.Geometry))
                            address.Building = building;
                    }
                }
            }
        }

        if (pseArray.Any())
        {
            foreach (var pse in pseArray)
            {
                var pseGeometry = Reader.Read<FeatureCollection>(pse.GeoJson);
                var pseFeature = pseGeometry.FirstOrDefault();
                if (buffer.Intersects(pseFeature!.Geometry))
                {
                    address.PlaningStructureElement = pse;
                    var buildingsOnPse = buildings.Where(building => building.PlaningStructureElement == pse).ToArray();

                    foreach (var building in buildingsOnPse)
                    {
                        var buildingGeometry = Reader.Read<FeatureCollection>(building.GeoJson);
                        var buildingFeature = buildingGeometry.First();
                        if (buffer.Intersects(buildingFeature.Geometry))
                            address.Building = building;
                    }
                }
            }
        }
    }
}