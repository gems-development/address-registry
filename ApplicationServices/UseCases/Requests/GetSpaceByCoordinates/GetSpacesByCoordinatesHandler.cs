using Gems.AddressRegistry.DataAccess;
using Gems.AddressRegistry.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationServices.UseCases.Requests.GetSpaceByCoordinates
{
    public class GetSpacesByCoordinatesHandler
    {
        private readonly IAppDbContext _context;
        public GetSpacesByCoordinatesHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Space>> Handle(GetSpacesByCoordinatesRequest request, CancellationToken cancellationToken)
        {
            return await _context.Spaces.ToArrayAsync(cancellationToken);
        }
    }
}
