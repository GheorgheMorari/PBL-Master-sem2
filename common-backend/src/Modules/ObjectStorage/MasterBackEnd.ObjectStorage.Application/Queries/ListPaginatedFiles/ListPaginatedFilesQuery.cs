using FluentResults;
using MasterBackEnd.ObjectStorage.Application.Responses;
using MediatR;

namespace MasterBackEnd.ObjectStorage.Application.Queries.ListPaginatedFiles
{
     public class ListPaginatedFilesQuery : IRequest<Result<ListedFilesResponse>>
     {
          public ListPaginatedFilesQuery(int currentPage, int pageSize)
          {
               CurrentPage = currentPage;
               PageSize = pageSize;
          }

          public int CurrentPage { get; set; } = 1;

          public int PageSize { get; set; } = 10;
     }
}
