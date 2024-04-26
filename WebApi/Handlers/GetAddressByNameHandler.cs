using Gems.AddressRegistry.DataAccess;
using Gems.AddressRegistry.Entities;
using MediatR;
using WebApi.Dto.Response;
using WebApi.MediatrRequests;

namespace WebApi.Handlers;

public class GetAddressByNameHandler : IRequestHandler<GetAddressByNameRequest, AddressDtoResponse>
{
    private readonly IAppDbContext _context;
    public GetAddressByNameHandler(IAppDbContextFactory appDbContextFactory)
    {
        _context = appDbContextFactory.Create();
    }
    public async Task<AddressDtoResponse> Handle(GetAddressByNameRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}