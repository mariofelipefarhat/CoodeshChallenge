using Coodesh.Application.Common.Responses;
using Coodesh.Infrastructure;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TransactionModel = Coodesh.Infrastructure.Models.Transaction;

namespace Coodesh.Application.Queries;

public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, List<TransactionQueryResponse>>
{
    private readonly TransactionContext  _ctx;
    private readonly IValidator<GetAllTransactionsQuery> _validator;

    public GetAllTransactionsQueryHandler(TransactionContext ctx, IValidator<GetAllTransactionsQuery> validator)
    {
        _ctx = ctx;
        _validator = validator;
    }

    public async Task<List<TransactionQueryResponse>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            IQueryable<TransactionModel> query = _ctx.Transactions.AsQueryable();

            if (request.SortDirection.ToLower() == "desc")
                query = query.OrderByDescending(x => EF.Property<object>(x, request.SortBy));

            else
                query = query.OrderBy(x => EF.Property<object>(x, request.SortBy));

            query = query.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize);

            List<TransactionModel> dbTransactions = await query.ToListAsync(cancellationToken);
            List<TransactionQueryResponse> transactionResponse = new();

            foreach (var dbTransaction in dbTransactions)
            {
                TransactionQueryResponse transaction = new(
                    dbTransaction.Id,
                    (TransactionTypeResponse)dbTransaction.Type,
                    dbTransaction.Date,
                    dbTransaction.Product,
                    dbTransaction.Amount,
                    dbTransaction.Seller,
                    (TransactionKindResponse)dbTransaction.Kind);

                transactionResponse.Add(transaction);
            }

            return transactionResponse;
        }

        return new List<TransactionQueryResponse>();
    }
}
