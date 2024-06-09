using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBackEnd.ObjectStorage.Application.Responses
{
     public class FileResponse
     {
          public FileResponse(byte[] data, string fileName, string fileType)
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
