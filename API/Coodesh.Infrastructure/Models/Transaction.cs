using Coodesh.Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;

namespace Coodesh.Infrastructure.Models;

public class Transaction : BaseEntity
{
    public Transaction(Guid id, TransactionType type, DateTime date, string product, int amount, string seller) : base(id)
    {
        Type = type;
        Date = date;
        Product = product;
        Amount = amount;
        Seller = seller;
    }

    [Required]
    public TransactionType Type { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public required string Product { get; set; }

    [Required]
    public int Amount { get; set; }

    [Required]
    public string Seller { get; set; }
}

public enum TransactionType
{
    SaleProducer = 1,
    SaleAffiliate = 2,
    CommissionPaid = 3,
    CommissionReceived = 4
}