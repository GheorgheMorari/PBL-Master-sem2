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
     public class IdentityController : ControllerBase
     {
          private readonly IMediator _mediator;

          public IdentityController(IMediator mediator)
          {
               _mediator = mediator;
          }

          [HttpPost]
          [Route("login")]
          [ProducesResponseType(StatusCodes.Status200OK)]
          [ProducesResponseType(StatusCodes.Status400BadRequest)]
          [ProducesResponseType(StatusCodes.Status500InternalServerError)]
          public async Task<ActionResult> Login([FromBody] LoginRequest request)
           => Ok(await _mediator.Send(new LoginQuery(request.UserName, request.Password)));

          [HttpPost]
          [Route("register")]
          [ProducesResponseType(StatusCodes.Status200OK)]
          [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
          [ProducesResponseType(StatusCodes.Status500InternalServerError)]
          public async Task<IActionResult> Register([FromBody] RegisterRequest request) =>
              Ok(await _mediator.Send(new RegisterUserCommand(request.UserName, request.Password, request.Email,
                  request.PhoneNumber)));
     }
}

