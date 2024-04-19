using MediatR;
using WebApi.Dto.Request;
using WebApi.Dto.Response;

namespace WebApi.MediatrRequests;

public record GetAddressByLocationRequest(GetAddressByLocationDto GetAddressByLocationDto) : IRequest<AddressDtoResponse>;