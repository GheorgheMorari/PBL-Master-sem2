using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBackEnd.Identity.Contracts
{
     public interface IUserService
     {
          Task<UserDto> GetUserDetails(string userId);
     }
}
