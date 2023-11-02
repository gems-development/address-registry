using Gems.AddressRegistry.DataAccess;
using Gems.AddressRegistry.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationServices.UseCases.Requests.GetSpaceByCoordinates
{
    internal class GetSpacesByCoordinatesHandler
    {
        private readonly IAppDbContext _context;
        public GetSpacesByCoordinatesHandler(IAppDbContextFactory appDbContextFactory)
        {
            _context = appDbContextFactory.Create();
        }
        public async Task<IEnumerable<Space>> Handle(GetSpacesByCoordinatesRequest request, CancellationToken cancellationToken)
        {
            return await _context.Spaces.Where(Space => Space.GeoJson == request.GeoJson).ToArrayAsync(cancellationToken);
        }
    }
}
