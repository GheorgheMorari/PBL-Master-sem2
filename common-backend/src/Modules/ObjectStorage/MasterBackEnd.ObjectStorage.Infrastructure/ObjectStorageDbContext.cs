using MasterBackEnd.ObjectStorage.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MasterBackEnd.ObjectStorage.Infrastructure
{
     public class ObjectStorageDbContext : DbContext
     {
          public ObjectStorageDbContext(DbContextOptions<ObjectStorageDbContext> options) : base(options)
          {
          }

          protected override void OnModelCreating(ModelBuilder modelBuilder)
          {
               modelBuilder.HasDefaultSchema("ObjectStorage");

               base.OnModelCreating(modelBuilder);
          }

          public DbSet<FileMetadataModel> FileMetadata { get; set; }
     }
}
