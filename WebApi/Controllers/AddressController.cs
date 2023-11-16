using Microsoft.AspNetCore.Mvc;
using MediatR;
using ApplicationServices.UseCases.Requests.GetAddressById;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly ISender _sender;

        public AddressController(ISender sender) 
        {
            _sender = sender;
        }

        [HttpGet ("{addressId:guid}")]
        public Task<AddressResponseDto> Get(Guid addressId, CancellationToken cancellationToken)
        {
           return _sender.Send(new GetAddressByIdRequest(addressId), cancellationToken);
        }
    }

}


