using MediatR;
using WebApi.Dto.Response;

namespace WebApi.MediatrRequests
{
    public record GetAddressByIdRequest(Guid AddressId) : IRequest<AddressDtoResponse>;
}
