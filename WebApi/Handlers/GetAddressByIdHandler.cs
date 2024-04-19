using Gems.AddressRegistry.DataAccess;
using MediatR;
using WebApi.Dto.Response;
using WebApi.Exceptions;
using WebApi.MediatrRequests;

namespace WebApi.Handlers
{
    internal class GetAddressByIdHandler : IRequestHandler<GetAddressByIdRequest, AddressDtoResponse>
    {
        private readonly IAppDbContext _context;
        
        public GetAddressByIdHandler(IAppDbContextFactory appDbContextFactory)
        {
            _context = appDbContextFactory.Create();
        }
        
        public async Task<AddressDtoResponse> Handle(GetAddressByIdRequest request, CancellationToken cancellationToken)
        {
            var foundedAddress = await _context.Addresses.FindAsync(new object[] { request.AddressId }, cancellationToken)
                ?? throw new EntityNotFoundException(request.AddressId);

            var addressResponseDto = new AddressDtoResponse(foundedAddress);
            return addressResponseDto;
        }
    }
}
