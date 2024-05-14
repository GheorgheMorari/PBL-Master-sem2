using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBackEnd.Identity.Application.Commands.Register
{
     public class RegisterUserCommand : IRequest<Unit>
     {
          public RegisterUserCommand(string userName, string password, string email, string phoneNumber)
          {
               UserName = userName;
               Password = password;
               Email = email;
               PhoneNumber = phoneNumber;
          }

          public string UserName { get; }
          public string Password { get; }
          public string Email { get; }
          public string PhoneNumber { get; }
     }
}
