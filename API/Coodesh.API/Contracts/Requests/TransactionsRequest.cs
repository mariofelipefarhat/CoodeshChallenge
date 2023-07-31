namespace Coodesh.API.Contracts.Requests
{
    public record TransactionsRequest(int Page = 1, int PageSize = 10, string SortBy = "Date", string SortDirection = "asc");
}
