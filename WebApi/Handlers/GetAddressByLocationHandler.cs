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
        
        var reader = new GeoJsonReader();

        var regions = _context.Regions.ToList();
        var intersectedRegions = new List<Region>();

        foreach (var region in regions)
        {
            var regionGeometry = reader.Read<FeatureCollection>(region.GeoJson);
            var regionFeature = regionGeometry.FirstOrDefault();
            if (buffer.Intersects(regionFeature!.Geometry))
            {
                intersectedRegions.Add(region);
            }
        }
        
        var addressResponseDto = new AddressDtoResponse(new Address());
        return addressResponseDto;
    }
}