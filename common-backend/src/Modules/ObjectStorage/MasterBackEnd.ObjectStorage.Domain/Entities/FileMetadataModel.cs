using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBackEnd.ObjectStorage.Domain.Entities
{
     public class FileMetadataModel
     {
          public Guid Id { get; set; }


          public Guid ObjectId { get; set; }

          public string FileName { get; set; }

          public string FileType { get; set; }

          public long FileSize { get; set; }

          public Guid UserId { get; set; }

     }
}
