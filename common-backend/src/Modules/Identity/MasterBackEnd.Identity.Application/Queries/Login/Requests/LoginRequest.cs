using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBackEnd.Identity.Application.Queries.Login.Requests
{
     public class LoginRequest
     {
          public string UserName { get; set; }
          public string Password { get; set; }
     }
}
