using MasterBackEnd.Identity.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBackEnd.Identity.Application.Queries.GetUserDetails
{
     public class GetUserDetailsQuery : IRequest<UserDto>
     {
          public GetUserDetailsQuery(string userId)
          {
               UserId = userId;
          }
          public string UserId { get; }
     }
}
