using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBackEnd.ObjectStorage.Application.Responses
{
     public class ListedFilesResponse
     {
          public List<FileUrlResponse> Files { get; set; }
          public int TotalFiles { get; set; }
          public int PageNumber { get; set; }
          public int PageSize { get; set; }
     }
}
