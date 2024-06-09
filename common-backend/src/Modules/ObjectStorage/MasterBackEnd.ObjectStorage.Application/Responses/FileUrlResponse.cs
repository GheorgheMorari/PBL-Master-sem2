using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBackEnd.ObjectStorage.Application.Responses
{
     public class FileUrlResponse
     {
          public Guid Id { get; set; }
          public string FileName { get; set; }
          public string FileType { get; set; }
          public string Url { get; set; }
     }
}
