using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechTask.AA.API.DTO;
using TechTask.AA.Application.Commands;

namespace TechTask.AA.API.Controllers
{
    /// <summary>
    /// Authorization controller
    /// </summary>
    public class IdentityController : BaseController
    {
        public IdentityController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        /// <summary>
        /// Authorization method, returns JWT with username and role
        /// </summary>
        /// <param name="dto">Input DTO with username and password</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("Authorize")]
        [ProducesResponseType(typeof(IdentityDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status500InternalServerError)]
        public async Task<IdentityDto> Authorize([FromQuery] AuthorizeDto dto, CancellationToken cancellationToken)
        {
            var command = MapData<AuthorizeDto, AuthorizeCommand>(dto);

            var result = await SendCommand(command, cancellationToken);

            var identity = MapData<AuthorizeCommand.Result, IdentityDto>(result);

            return identity;
        }
    }
}
