using Gems.AddressRegistry.DataAccess;
using Gems.AplicationServices.Exceptions;
using MediatR;

namespace Gems.ApplicationServices.UseCases.Requests.GetAddressById
{
    internal class GetAddressByIdHandler : IRequestHandler<GetAddressByIdRequest, AddressResponseDto>
    {
        private readonly IAppDbContext _context;
        public GetAddressByIdHandler(IAppDbContextFactory appDbContextFactory)
        {
            _context = appDbContextFactory.Create();
        }
        public async Task<AddressResponseDto> Handle(GetAddressByIdRequest request, CancellationToken cancellationToken)
        {
            var foundAddress = await _context.Addresses.FindAsync(new object[] { request.addressId }, cancellationToken)
                ?? throw new EntityNotFoundException(request.addressId);
            //    var foundAddress = await _context
            //.Addresses
            //.Include(o => o.Country)
            //.FirstOrDefaultAsync(o => o.Id == request.addressId, cancellationToken)

            AddressResponseDto addressResponseDto = new AddressResponseDto(foundAddress);
            return addressResponseDto;
        }
    }
}
