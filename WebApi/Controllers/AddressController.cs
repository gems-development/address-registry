using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.Dto.Request;
using WebApi.MediatrRequests;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/addresses/[action]")]
    public class AddressController : ControllerBase
    {
        private readonly ISender _sender;

        public AddressController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{addressId:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddressById(Guid addressId, CancellationToken cancellationToken)
        {
            var addressDto = await _sender.Send(new GetAddressByIdRequest(addressId), cancellationToken);
            return StatusCode(StatusCodes.Status200OK, addressDto);
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddressByLocation([FromHeader] GetAddressByLocationDto getAddressByLocationDto, 
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var addressDto = await _sender.Send(new GetAddressByLocationRequest(getAddressByLocationDto), cancellationToken);
            return StatusCode(StatusCodes.Status200OK, addressDto);
        }

        [HttpGet("{addressName}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddressByName(string? addressName, CancellationToken cancellationToken)
        {
            if (addressName is null)
                return BadRequest("Wrong addressName");
            
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}


