using MasterBackEnd.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MasterBackEnd.Identity.Domain;

public class IdentityDbContext : IdentityDbContext<User, Role, Guid>
{
     public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
         : base(options)
     {
     }

     protected override void OnModelCreating(ModelBuilder builder)
     {
          base.OnModelCreating(builder);
     }
}
