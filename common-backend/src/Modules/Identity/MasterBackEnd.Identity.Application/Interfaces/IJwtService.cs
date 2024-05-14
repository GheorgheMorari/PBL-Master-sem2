using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MasterBackEnd.Identity.Application.Interfaces
{
     public interface IJwtService
     {
          string GenerateJwt(List<Claim> claims);
     }
}
