using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TechTask.AA.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Create a new instance of <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="mediator"></param>
        public BaseController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        protected Task<TResponse> SendCommand<TResponse>(IRequest<TResponse> command, CancellationToken cancellationToken)
        {
            return _mediator.Send(command, cancellationToken);
        }

        protected TOut MapData<TIn, TOut>(TIn data)
        {
            return _mapper.Map<TOut>(data);
        }
    }
}
