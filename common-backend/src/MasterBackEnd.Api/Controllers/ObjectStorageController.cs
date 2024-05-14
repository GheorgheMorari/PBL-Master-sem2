using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBackEnd.Api.Controllers
{
     public class ObjectStorageController : ControllerBase
     {
          private readonly IMediator _mediator;
          public ObjectStorageController(IMediator mediator)
          {
               _mediator = mediator;
          }

          public async Task<IActionResult> UploadFile(string fileName, CancellationToken cancellationToken)
          {
               var result = await _mediator.Send(fileName, cancellationToken);

               return Ok(result);
          }
     }
}
