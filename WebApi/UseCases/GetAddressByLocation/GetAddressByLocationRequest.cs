using MediatR;
using WebApi.Dto;

namespace WebApi.UseCases.GetAddressByLocation;

public readonly record struct GetAddressByLocationRequest(GetAddressByLocationDto GetAddressByLocationDto) : IRequest<AddressDtoResponse>;