namespace Coodesh.API.Contracts.Requests
{
    public record TransactionsRequest(int Page = 1, int PageSize = 100, string SortBy = "Date", string SortDirection = "asc");
}
