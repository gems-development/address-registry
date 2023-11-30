using MediatR;

namespace Gems.ApplicationServices.UseCases.Requests.GetAddressById
{
    public record GetAddressByIdRequest(Guid addressId) : IRequest<AddressResponseDto>;

}
