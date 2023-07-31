using Coodesh.Application.Common.Responses;
using MediatR;

namespace Coodesh.Application.Queries
{
    public record GetAllTransactionsQuery(int Page, int PageSize, string SortBy, string SortDirection) : IRequest<List<TransactionQueryResponse>>;
}
