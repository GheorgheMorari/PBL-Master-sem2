using FluentResults;
using MasterBackEnd.ObjectStorage.Domain.Entities;
using MasterBackEnd.ObjectStorage.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Minio;
using Minio.DataModel.Args;
using System.Security.Claims;

namespace MasterBackEnd.ObjectStorage.Application.Commands.UploadFile
{
     public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, Result>
     {
          private readonly IMinioClient _minioClient;
          private readonly ObjectStorageDbContext _context;
          private readonly IHttpContextAccessor _httpContext;

          private const string _defaultBucketName = "private";

          public UploadFileCommandHandler(IMinioClient minioClient, ObjectStorageDbContext context,
               IHttpContextAccessor httpContext)
          {
               _minioClient = minioClient;
               _context = context;
               _httpContext = httpContext;
          }

          public async Task<Result> Handle(UploadFileCommand request, CancellationToken token)
          {
               using (var stream = new MemoryStream(request.Data))
               {

                    bool bucketExists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_defaultBucketName));
                    if (!bucketExists)
                    {
                         await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_defaultBucketName));
                    }

                    var minioName = Guid.NewGuid();

                    var addFile = await _minioClient.PutObjectAsync(new PutObjectArgs()
                                                        .WithBucket(_defaultBucketName)
                                                        .WithObject(minioName.ToString())
                                                        .WithStreamData(stream)
                                                        .WithObjectSize(stream.Length)
                                                        .WithContentType(request.FileType));

                    if (!string.IsNullOrEmpty(addFile.Etag))
                    {
                         var metadata = new FileMetadataModel
                         {
                              FileName = request.FileName,
                              FileSize = stream.Length,
                              FileType = request.FileType,
                              ObjectId = minioName,
                              UserId = Guid.Parse(_httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId").Value)
                         };

                         await _context.FileMetadata.AddAsync(metadata);
                         await _context.SaveChangesAsync();
                         return Result.Ok();
                    }

                    return Result.Fail("Failed to upload file");
               }
          }
     }
}
