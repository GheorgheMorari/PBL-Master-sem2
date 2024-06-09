using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBackEnd.ObjectStorage.Application.Commands
{
     public class DeleteFileCommand : IRequest<Result>
     {
          public DeleteFileCommand(Guid id)
          {
               Id = id;
          }

          public Guid Id { get; set; }
     }
}
