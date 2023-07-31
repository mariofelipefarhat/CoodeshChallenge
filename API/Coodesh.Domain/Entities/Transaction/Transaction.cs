namespace Coodesh.Domain.Entities.Product;

public sealed class Transaction
{
    public Transaction(TransactionType type, DateTime date, string product, int amount, string seller)
    {
        Type = type;
        Date = date;
        Product = product ?? throw new ArgumentNullException(nameof(product));
        Amount = amount;
        Seller = seller ?? throw new ArgumentNullException(nameof(seller));
    }

    public TransactionType Type { get; set; }
    public DateTime Date { get; set; }
    public required string Product { get; set; }
    public int Amount { get; set; }
    public string Seller { get; set; }
}

public enum TransactionType
{
    SaleProducer = 1,
    SaleAffiliate = 2,
    CommissionPaid = 3,
    CommissionReceived = 4
}
