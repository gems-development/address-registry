using MediatR;
using WebApi.Dto.Response;

namespace WebApi.MediatrRequests;

public record GetAddressByNameRequest(string AddressName) : IRequest<AddressDtoResponse>;