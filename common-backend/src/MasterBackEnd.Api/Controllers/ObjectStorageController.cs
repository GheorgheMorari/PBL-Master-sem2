using MasterBackEnd.ObjectStorage.Application.Commands;
using MasterBackEnd.ObjectStorage.Application.Queries.DownloadFile;
using MasterBackEnd.ObjectStorage.Application.Queries.ListPaginatedFiles;
using MasterBackEnd.ObjectStorage.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasterBackEnd.Api.Controllers
{
     [ApiController]
     [Authorize]
     [Route("api/ObjectStorage/[action]")]
     public class ObjectStorageController : ControllerBase
     {
          private readonly IMediator _mediator;

          public ObjectStorageController(IMediator mediator)
          {
               _mediator = mediator;
          }

          [HttpPost]
          public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken cancellationToken = default)
          {
               if (file == null || file.Length == 0)
               {
                    return BadRequest("File is not provided or is empty.");
               }

               byte[] fileData;
               using (var memoryStream = new MemoryStream())
               {
                    await file.CopyToAsync(memoryStream);
                    fileData = memoryStream.ToArray();
               }

               var command = new UploadFileCommand(file.FileName, file.ContentType, fileData);
               var result = await _mediator.Send(command, cancellationToken);

               if (result.IsSuccess)
               {
                    return Ok();
               }

               return BadRequest(result.Errors);
          }

          [HttpDelete]
          public async Task<IActionResult> DeleteFile(Guid id, CancellationToken cancellationToken = default)
          {
               var result = await _mediator.Send(new DeleteFileCommand(id), cancellationToken);

               if (result.IsSuccess)
               {
                    return Ok();
               }

               return BadRequest(result.Errors);
          }

          [HttpGet]
          public async Task<IActionResult> ListPaginatedFiles(int currentPage = 1, int pageSize = 10,
               CancellationToken cancellationToken = default)
          {
               var result = await _mediator.Send(new ListPaginatedFilesQuery(currentPage, pageSize), cancellationToken);

               if (result.IsSuccess)
               {
                    return Ok(result.Value);
               }

               return BadRequest(result.Errors);
          }

          [HttpGet]
          public async Task<IActionResult> DownloadFile(Guid id,
               CancellationToken cancellationToken = default)
          {
               var result = await _mediator.Send(new DownloadFileQuery(id), cancellationToken);
               if (result.IsSuccess)
               {
                    return File(result.Value.Data, result.Value.FileType, result.Value.FileName);
               }

               return BadRequest(result.Errors);
          }
     }
}
