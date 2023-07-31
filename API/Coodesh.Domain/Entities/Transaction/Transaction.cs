namespace Coodesh.Domain.Entities.Product;

public sealed class Transaction
{
    public Transaction(TransactionType type, DateTime date, string product, decimal amount, string seller)
    {
        Type = type;
        Date = date;
        Product = product;
        Amount = amount;
        Seller = seller;
    }

    public TransactionType Type { get; }
    public DateTime Date { get; }
    public string Product { get; }
    public decimal Amount { get; }
    public string Seller { get; }
}

public enum TransactionType
{
    SaleProducer = 1,
    SaleAffiliate = 2,
    CommissionPaid = 3,
    CommissionReceived = 4
}
