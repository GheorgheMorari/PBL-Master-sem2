using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBackEnd.Identity.Application.Queries.Responses
{
     public class LoginResponse
     {
          public LoginResponse(string token)
          {
               Token = token;
          }
          public string Token { get; }

     }
}
