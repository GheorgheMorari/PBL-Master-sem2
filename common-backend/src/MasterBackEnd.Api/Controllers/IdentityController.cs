using MasterBackEnd.Identity.Application.Commands.Register;
using MasterBackEnd.Identity.Application.Commands.Register.Requests;
using MasterBackEnd.Identity.Application.Queries.GetUserDetails;
using MasterBackEnd.Identity.Application.Queries.Login;
using MasterBackEnd.Identity.Application.Queries.Login.Requests;
using MasterBackEnd.Identity.Contracts;
using MasterBackEnd.Infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace MasterBackEnd.Api.Controllers
{
     [Authorize]
     public class IdentityController : ControllerBase
     {
          private readonly IMediator _mediator;

          public IdentityController(IMediator mediator)
          {
               _mediator = mediator;
          }

          [HttpPost]
          [AllowAnonymous]
          [ProducesResponseType(StatusCodes.Status200OK)]
          [ProducesResponseType(StatusCodes.Status400BadRequest)]
          [ProducesResponseType(StatusCodes.Status500InternalServerError)]
          [Route("login")]
          public async Task<ActionResult> Login(LoginRequest request)
           => Ok(await _mediator.Send(new LoginQuery(request.UserName, request.Password)));

          [HttpPost]
          [AllowAnonymous]
          [ProducesResponseType(StatusCodes.Status200OK)]
          [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
          [ProducesResponseType(StatusCodes.Status500InternalServerError)]
          [Route("register")]
          public async Task<IActionResult> Register(RegisterRequest request) =>
              Ok(await _mediator.Send(new RegisterUserCommand(request.UserName, request.Password, request.Email,
                  request.PhoneNumber)));


          [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
          [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
          [ProducesResponseType(StatusCodes.Status500InternalServerError)]
          [Authorize]
          [HttpGet]
          [Route("{userId}")]
          public async Task<ActionResult<UserDto>> GetUserDetails(string userId, CancellationToken cancellationToken) =>
              Ok(await _mediator.Send(new GetUserDetailsQuery(userId), cancellationToken));
     }
}

