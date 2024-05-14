using MasterBackEnd.Identity.Application.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBackEnd.Identity.Application.Queries.Login
{
     public class LoginQuery : IRequest<LoginResponse>
     {
          public LoginQuery(string userName, string password)
          {
               UserName = userName;
               Password = password;
          }
          public string UserName { get; }
          public string Password { get; }
     }
}
