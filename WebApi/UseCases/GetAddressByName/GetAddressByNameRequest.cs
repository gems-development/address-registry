using MediatR;
using WebApi.Dto;

namespace WebApi.UseCases.GetAddressByName;

public readonly record struct GetAddressByNameRequest(string AddressName) : IRequest<AddressDtoResponse>;