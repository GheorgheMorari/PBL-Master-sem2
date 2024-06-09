using MasterBackEnd.ObjectStorage.Application.Responses;
using MediatR;

namespace MasterBackEnd.ObjectStorage.Application.Queries.DownloadFile
{
     public class DownloadFileQuery : IRequest<FileResponse>
     {
          public DownloadFileQuery(Guid id)
          {
               Id = id;
          }

          public Guid Id { get; set; }
     }
}
