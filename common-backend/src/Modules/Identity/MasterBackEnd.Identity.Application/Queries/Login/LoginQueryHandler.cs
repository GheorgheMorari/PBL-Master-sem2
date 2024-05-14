using MasterBackEnd.Identity.Application.Interfaces;
using MasterBackEnd.Identity.Application.Queries.Responses;
using MasterBackEnd.Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MasterBackEnd.Identity.Application.Queries.Login
{
     public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponse>
     {
          private readonly UserManager<User> _userManager;
          private readonly IJwtService _jwtService;

          public LoginQueryHandler(UserManager<User> userManager, IJwtService jwtService)
          {
               _userManager = userManager;
               _jwtService = jwtService;
          }
          public async Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
          {
               var user = await _userManager.FindByNameAsync(request.UserName);

               if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
               {
                    throw new Exception("Invalid password or username");
               }

               var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role,"User"),
                new Claim("UserId",user.Id.ToString())
            };

               return new LoginResponse(_jwtService.GenerateJwt(claims));
          }
     }
}
