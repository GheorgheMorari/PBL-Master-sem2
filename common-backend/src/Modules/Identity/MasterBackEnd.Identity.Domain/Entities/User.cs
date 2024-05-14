using Microsoft.AspNetCore.Identity;

namespace MasterBackEnd.Identity.Domain.Entities;

public class User : IdentityUser<Guid>
{
     public User()
     {

     }

     public User(string username, string email, string phoneNumber)
     {
          UserName = username;
          Email = email;
          PhoneNumber = phoneNumber;
     }
}

