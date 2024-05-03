using Gems.AddressRegistry.DataAccess;
using MediatR;
using WebApi.Dto;

namespace WebApi.UseCases.GetAddressByName;

public class GetAddressByNameRequestHandler : IRequestHandler<GetAddressByNameRequest, AddressDtoResponse>
{
    private readonly IAppDbContext _context;
    public GetAddressByNameRequestHandler(IAppDbContextFactory appDbContextFactory)
    {
        _context = appDbContextFactory.Create();
    }
    public async Task<AddressDtoResponse> Handle(GetAddressByNameRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}