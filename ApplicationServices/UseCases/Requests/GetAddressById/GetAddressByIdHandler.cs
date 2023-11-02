using ApplicationServices.UseCases.Requests.GetSpaceByCoordinates;
using Gems.AddressRegistry.DataAccess;
using Gems.AddressRegistry.Entities;
using MediatR;

namespace ApplicationServices.UseCases.Requests.GetAddressById
{
    internal class GetAddressByIdHandler
    {
        private readonly IAppDbContext _context;
        public GetAddressByIdHandler(IAppDbContextFactory appDbContextFactory)
        {
            _context = appDbContextFactory.Create();
        }
        public async Task<AddressResponseDto> Handle(GetAddressByIdRequest request, CancellationToken cancellationToken)
        {
            var foundAddress = await _context.Addresses.FindAsync(new object[] { request.addressId }, cancellationToken)
                ??throw new Exception(request.addressId.ToString());
            AddressResponseDto addressResponseDto = new AddressResponseDto(foundAddress);
            return addressResponseDto;
        }
    }
}
