using MasterBackEnd.ObjectStorage.Application.Responses;
using MediatR;

namespace MasterBackEnd.ObjectStorage.Application.Queries.ListPaginatedFiles
{
    public class ListPaginatedFilesQuery : IRequest<FileResponse>
    {
          public ListPaginatedFilesQuery() { }

          public int CurrentPage { get; set; }

          public int PageSize { get; set; } = 10;
    }
}
