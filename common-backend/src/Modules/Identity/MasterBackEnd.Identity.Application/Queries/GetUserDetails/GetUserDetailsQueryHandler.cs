using MasterBackEnd.Identity.Contracts;
using MasterBackEnd.Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBackEnd.Identity.Application.Queries.GetUserDetails
{
     public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, UserDto>, IUserService
     {
          private readonly UserManager<User> _userManager;
          public GetUserDetailsQueryHandler(UserManager<User> userManager)
          {
               _userManager = userManager;
          }
          public async Task<UserDto> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
          {
               var user = await _userManager.FindByIdAsync(request.UserId);
               if (user is null)
                    throw new Exception("User not found");

               return new UserDto(user.UserName, user.Email, user.PhoneNumber);
          }

          public Task<UserDto> GetUserDetails(string userId)
          {
               return Handle(new GetUserDetailsQuery(userId), CancellationToken.None);
          }
     }
}
