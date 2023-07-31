namespace Coodesh.Application.Common.Responses;

public record TransactionQueryResponse(
    Guid Id,
    TransactionTypeResponse Type,
    DateTime Date,
    string Product,
    decimal Amount,
    string Seller,
    TransactionKindResponse Kind);
public enum TransactionTypeResponse
{
    SaleProducer = 1,
    SaleAffiliate = 2,
    CommissionPaid = 3,
    CommissionReceived = 4
}
public enum TransactionKindResponse
{
    MoneyIn = 1,
    MoneyOut = 2,
}
