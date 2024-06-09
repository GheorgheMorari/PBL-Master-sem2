using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MasterBackEnd.ObjectStorage.Infrastructure.Startup
{
     public static class ObjectStorageModuleStartup
     {
          public static IServiceCollection AddObjectStorageModule(this IServiceCollection services,
              IConfiguration configuration)
          {

               var connectionString = configuration.GetConnectionString("MasterDbContextConnection")
                    ?? throw new InvalidOperationException("Connection string 'MasterDbContextConnection' not found.");

               services.AddDbContext<ObjectStorageDbContext>(options => options.UseNpgsql(connectionString));
              
               return services;
          }
     }
}
