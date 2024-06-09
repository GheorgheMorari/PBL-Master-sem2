
using FluentResults;
using MediatR;

namespace MasterBackEnd.ObjectStorage.Application.Commands
{
     public class UploadFileCommand : IRequest<Result>
     {
          public UploadFileCommand(string fileName, string fileType, byte[] data)
          {
               FileName = fileName;
               FileType = fileType;
               Data = data;
          }

          public string FileName { get; set; }

          public string FileType { get; set; }

          public byte[] Data { get; set; }
     }
}
