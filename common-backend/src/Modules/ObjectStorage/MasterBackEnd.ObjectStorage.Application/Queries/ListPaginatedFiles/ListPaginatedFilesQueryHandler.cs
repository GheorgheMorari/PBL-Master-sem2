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

namespace MasterBackEnd.ObjectStorage.Application.Queries.ListPaginatedFiles
{
     public class ListPaginatedFilesQueryHandler : IRequestHandler<ListPaginatedFilesQuery, Result<ListedFilesResponse>>
     {
          private readonly IMinioClient _minioClient;
          private readonly ObjectStorageDbContext _context;
          private readonly IHttpContextAccessor _httpContext;

          private const string _defaultBucketName = "private";

          public ListPaginatedFilesQueryHandler(IMinioClient minioClient, ObjectStorageDbContext context, IHttpContextAccessor httpContext)
          {
               _minioClient = minioClient;
               _context = context;
               _httpContext = httpContext;
          }

          public async Task<Result<ListedFilesResponse>> Handle(ListPaginatedFilesQuery request, CancellationToken cancellationToken)
          {
               var userIdClaim = _httpContext.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == "UserId");

               if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var currentUserId))
               {
                    return Result.Fail("Unauthorized");
               }

               var userFilesQuery = _context.FileMetadata
                   .Where(x => x.UserId == currentUserId)
                   .OrderBy(x => x.Id);

               var totalFiles = await userFilesQuery.CountAsync(cancellationToken);
               var userFiles = await userFilesQuery
                   .Skip((request.CurrentPage - 1) * request.PageSize)
                   .Take(request.PageSize)
                   .ToListAsync(cancellationToken);

               var fileResponses = new List<FileUrlResponse>();

               foreach (var file in userFiles)
               {
                    var presignedUrl = await GetPresignedUrl(file.ObjectId.ToString());
                    fileResponses.Add(new FileUrlResponse
                    {
                         Id = userFilesQuery.FirstOrDefault(x => x.ObjectId == file.ObjectId).Id,
                         FileName = userFilesQuery.FirstOrDefault(x => x.ObjectId == file.ObjectId).FileName,
                         FileType = file.FileType,
                         Url = presignedUrl
                    });
               }

               var response = new ListedFilesResponse
               {
                    Files = fileResponses,
                    TotalFiles = totalFiles,
                    PageNumber = request.CurrentPage,
                    PageSize = request.PageSize
               };

               return Result.Ok(response);
          }

          private async Task<string> GetPresignedUrl(string objectId)
          {
               var presignedUrlArgs = new PresignedGetObjectArgs()
                   .WithBucket(_defaultBucketName)
                   .WithObject(objectId)
                   .WithExpiry(24 * 60 * 60);

               return await _minioClient.PresignedGetObjectAsync(presignedUrlArgs);
          }
     }
}
