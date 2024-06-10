using FluentResults;
using MasterBackEnd.ObjectStorage.Application.Responses;
using MasterBackEnd.ObjectStorage.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Minio;
using Minio.DataModel.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBackEnd.ObjectStorage.Application.Queries.DownloadFile
{
     public class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, Result<FileResponse>>
     {
          private readonly IMinioClient _minioClient;
          private readonly ObjectStorageDbContext _context;

          private const string _defaultBucketName = "private";

          public DownloadFileQueryHandler(IMinioClient minioClient, ObjectStorageDbContext context)
          {
               _minioClient = minioClient;
               _context = context;
          }

          public async Task<Result<FileResponse>> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
          {
               var metadata = await _context.FileMetadata.FirstOrDefaultAsync(x => x.Id == request.Id);

               if(metadata == null)
               {
                    return Result.Fail("Fail not found");
               }

               try
               {
                    var statObjectArgs = new StatObjectArgs()
                         .WithBucket(_defaultBucketName)
                         .WithObject(metadata.ObjectId.ToString());

                    var statObject = await _minioClient.StatObjectAsync(statObjectArgs);

                    using (var memoryStream = new MemoryStream())
                    {
                         var getObjectArgs = new GetObjectArgs()
                             .WithBucket(_defaultBucketName)
                             .WithObject(metadata.ObjectId.ToString())
                             .WithCallbackStream(async stream =>
                             {
                                  await stream.CopyToAsync(memoryStream);
                             });

                         await _minioClient.GetObjectAsync(getObjectArgs);
                         memoryStream.Seek(0, SeekOrigin.Begin);

                         var result = new FileResponse(memoryStream.ToArray(), metadata.FileName, metadata.FileType);

                         return Result.Ok(result);

                    };
               }
               catch (Exception ex)
               {
                    return Result.Fail("Something went wrong. File not found in MinioStorage");
               }
          }
     }
}
