using MediatR;

namespace ApplicationServices.UseCases.Requests.GetAddressById
{
    public record GetAddressByIdRequest(Guid addressId) : IRequest<AddressResponseDto>;
    
}
