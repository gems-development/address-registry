using Gems.AddressRegistry.Entities;
using MediatR;

namespace ApplicationServices.UseCases.Requests.GetSpaceByCoordinates
{
    public record GetSpacesByCoordinatesRequest(String GeoJson) : IRequest<IEnumerable<Space>>;
}
