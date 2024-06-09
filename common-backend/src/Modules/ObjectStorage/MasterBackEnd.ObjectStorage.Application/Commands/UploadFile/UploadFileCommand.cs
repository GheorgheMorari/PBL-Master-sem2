
using MediatR;

namespace MasterBackEnd.ObjectStorage.Application.Commands
{
     public class UploadFileCommand : IRequest<Unit>
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
