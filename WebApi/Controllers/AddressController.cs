using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.Dto.Request;
using WebApi.MediatrRequests;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/address")]
    public class AddressController : ControllerBase
    {
        private readonly ISender _sender;

        public AddressController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddressById(Guid id, CancellationToken cancellationToken)
        {
            var addressDto = await _sender.Send(new GetAddressByIdRequest(id), cancellationToken);
            return StatusCode(StatusCodes.Status200OK, addressDto);
        }

        [HttpGet("location")]
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

        [HttpGet("{name}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddressByName(string? name, CancellationToken cancellationToken)
        {
            if (name is null)
                return BadRequest("Wrong address_name");
            
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}


