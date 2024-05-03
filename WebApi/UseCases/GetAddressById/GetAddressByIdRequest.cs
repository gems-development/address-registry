using MediatR;
using WebApi.Dto;

namespace WebApi.UseCases.GetAddressById
{
    public readonly record struct GetAddressByIdRequest(Guid AddressId) : IRequest<AddressDtoResponse>;
}
