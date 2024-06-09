using FluentResults;
using MasterBackEnd.ObjectStorage.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Minio;
using Minio.DataModel.Args;

namespace MasterBackEnd.ObjectStorage.Application.Commands.DeleteFile
{
     public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, Result>
     {
          private readonly IMinioClient _minioClient;
          private readonly ObjectStorageDbContext _context;

          private const string _defaultBucketName = "private";

          public DeleteFileCommandHandler(IMinioClient minioClient, ObjectStorageDbContext context)
          {
               _minioClient = minioClient;
               _context = context;
          }

          public async Task<Result> Handle(DeleteFileCommand request, CancellationToken token)
          {
               var metadata = await _context.FileMetadata.FirstOrDefaultAsync(x => x.Id == request.Id);

               if (metadata == null)
               {
                    return Result.Fail("Something went wrong. This file doesn't exist");
               }

               try
               {
                    var checkObjectArgs = new StatObjectArgs()
                         .WithBucket(_defaultBucketName)
                         .WithObject(metadata.ObjectId.ToString());

                    var checkObjectResult = await _minioClient.StatObjectAsync(checkObjectArgs);

                    var removeFileArgs = new RemoveObjectArgs()
                         .WithBucket(_defaultBucketName)
                         .WithObject(metadata.ObjectId.ToString());

                    await _minioClient.RemoveObjectAsync(removeFileArgs);


                    var result = _context.FileMetadata.Remove(metadata);
                    await _context.SaveChangesAsync();

                    return Result.Ok();
               }
               catch (Minio.Exceptions.ObjectNotFoundException)
               {
                    return Result.Fail($"Object with ID {metadata.ObjectId} not found.");
               }
               catch (Exception ex)
               {
                    return Result.Fail(ex.Message);
               }
          }

     }
}
