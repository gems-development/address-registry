using Gems.AddressRegistry.DataAccess;
using MediatR;
using WebApi.Dto;
using WebApi.Exceptions;

namespace WebApi.UseCases.GetAddressById
{
    internal class GetAddressByIdRequestHandler : IRequestHandler<GetAddressByIdRequest, AddressDtoResponse>
    {
        private readonly IAppDbContext _context;
        
        public GetAddressByIdRequestHandler(IAppDbContextFactory appDbContextFactory)
        {
            _context = appDbContextFactory.Create();
        }
        
        public async Task<AddressDtoResponse> Handle(GetAddressByIdRequest request, CancellationToken cancellationToken)
        {
            var foundAddress = await _context.Addresses.FindAsync(new object[] { request.AddressId }, cancellationToken)
                ?? throw new EntityNotFoundException(request.AddressId);

            var addressResponseDto = new AddressDtoResponse(foundAddress);
            return addressResponseDto;
        }
    }
}
