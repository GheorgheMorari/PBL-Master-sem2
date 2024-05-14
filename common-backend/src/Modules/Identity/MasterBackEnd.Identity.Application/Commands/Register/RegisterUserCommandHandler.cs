using MasterBackEnd.Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBackEnd.Identity.Application.Commands.Register
{
     public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Unit>
     {
          private readonly UserManager<User> _userManager;

          public RegisterUserCommandHandler(UserManager<User> userManager)
          {
               _userManager = userManager;
          }

          public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
          {
               var identity = await _userManager.CreateAsync(new User(request.UserName, request.Email, request.PhoneNumber),
                   request.Password);
               if (!identity.Succeeded)
                    throw new Exception(identity.Errors.FirstOrDefault()?.Description);

               return Unit.Value;
          }
     }
}