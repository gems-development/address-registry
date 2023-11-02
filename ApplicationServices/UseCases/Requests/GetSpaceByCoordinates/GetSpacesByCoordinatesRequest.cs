using MediatR;
using Gems.AddressRegistry.Entities;

namespace ApplicationServices.UseCases.Requests.GetSpaceByCoordinates
{
    public record GetSpacesByCoordinatesRequest : IRequest<IEnumerable<Space>>;
}
