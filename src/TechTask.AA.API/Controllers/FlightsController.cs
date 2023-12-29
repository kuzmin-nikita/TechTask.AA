using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechTask.AA.API.DTO;
using TechTask.AA.Application.Commands;
using TechTask.AA.Application.Queries;

namespace TechTask.AA.API.Controllers
{
    /// <summary>
    /// Flights controller
    /// </summary>
    [Authorize]
    public class FlightsController : BaseController
    {
        public FlightsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        /// <summary>
        /// Method for getting all the flights filtered by Origin and/or Destination 
        /// </summary>
        /// <param name="dto">Input DTO with Origin and/or Destination</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(FlightDto[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status500InternalServerError)]
        public async Task<FlightDto[]> GetFlights([FromQuery] GetFlightsDto dto, CancellationToken cancellationToken)
        {
            var query = MapData<GetFlightsDto, GetFlightsQuery>(dto);

            var result = await SendCommand(query, cancellationToken);

            var flights = result.Select(r => MapData<GetFlightsQuery.Result, FlightDto>(r)).ToArray();

            return flights;
        }

        /// <summary>
        /// Method for creating new flight
        /// </summary>
        /// <param name="dto">Input DTO with all data for flight creation</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(Roles = "Moderator")]
        [HttpPost]
        [ProducesResponseType(typeof(FlightDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status500InternalServerError)]
        public async Task<FlightDto> CreateFlight([FromBody] CreateFlightDto dto, CancellationToken cancellationToken)
        {
            var command = MapData<CreateFlightDto, CreateFlightCommand>(dto);

            var result = await SendCommand(command, cancellationToken);

            var flight = MapData<CreateFlightCommand.Result, FlightDto>(result);

            return flight;
        }

        /// <summary>
        /// Method for updating flight status
        /// </summary>
        /// <param name="dto">Input DTO with flight Id and new status</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(Roles = "Moderator")]
        [HttpPatch]
        [ProducesResponseType(typeof(FlightDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status500InternalServerError)]
        public async Task<FlightDto> UpdateFlightStatus([FromBody] UpdateFlightStatusDto dto, CancellationToken cancellationToken)
        {
            var command = MapData<UpdateFlightStatusDto, UpdateFlightStatusCommand>(dto);

            var result = await SendCommand(command, cancellationToken);

            var flight = MapData<UpdateFlightStatusCommand.Result, FlightDto>(result);

            return flight;
        }
    }
}
