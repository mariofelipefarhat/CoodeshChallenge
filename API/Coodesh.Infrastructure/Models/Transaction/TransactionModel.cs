using Coodesh.Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;

namespace Coodesh.Infrastructure.Models.Transaction;

public class TransactionModel : BaseEntity
{
    public TransactionModel(Guid id, TransactionTypeModel type, DateTime date, string product, decimal amount, string seller) : base(id)
    {
        Type = type;
        Date = date;
        Product = product;
        Amount = amount;
        Seller = seller;
        Kind = type == TransactionTypeModel.CommissionPaid ? Kind.MoneyOut : Kind.MoneyIn;
    }

    [Required]
    public TransactionTypeModel Type { get; private set; }

    [Required]
    public DateTime Date { get; private set; }

    [Required]
    public string Product { get; private set; }

    [Required]
    public decimal Amount { get; private set; }

    [Required]
    public string Seller { get; private set; }
    [Required]
    public Kind Kind { get; private set; }

    public void Update(TransactionTypeModel type, DateTime date, string product, decimal amount, string seller)
    {
        Type = type;
        Date = date;
        Product = product;
        Amount = amount;
        Seller = seller;
        Kind = type == TransactionTypeModel.CommissionPaid ? Kind.MoneyOut : Kind.MoneyIn;
    }
}

public enum TransactionTypeModel
{
    SaleProducer = 1,
    SaleAffiliate = 2,
    CommissionPaid = 3,
    CommissionReceived = 4
}

public enum Kind
{
    MoneyIn = 1,
    MoneyOut = 2,
}