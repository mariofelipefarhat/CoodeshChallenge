﻿
using Coodesh.Domain.Entities.Transaction;
using Coodesh.Infrastructure.Persistence.Transaction;
using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Text.RegularExpressions;
using TransactionEntity = Coodesh.Domain.Entities.Transaction.TransactionEntity;
using TransactionModel = Coodesh.Infrastructure.Models.Transaction.TransactionModel;
using TransactionTypeModel = Coodesh.Infrastructure.Models.Transaction.TransactionTypeModel;

namespace Coodesh.Application.Commands;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, ErrorOr<bool>>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IValidator<CreateTransactionCommand> _validator;
    public CreateTransactionCommandHandler(ITransactionRepository transactionRepository, IValidator<CreateTransactionCommand> validator)
    {
        _transactionRepository = transactionRepository;
        _validator = validator;
    }

    public async Task<ErrorOr<bool>> Handle(CreateTransactionCommand cmd, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(cmd, cancellationToken);

        if (validationResult.IsValid)
        {
            string LinePattern = @"^([1-4])(\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}-\d{2}:\d{2})(.*?\S)\s+(\d{10})(.*)$";

            Regex regex = new (LinePattern);

            var transactionLines = new List<TransactionEntity>();

            using var reader = new StreamReader(cmd.Stream.OpenReadStream());
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (string.IsNullOrEmpty(line))
                    continue;

                var match = regex.Match(line);

                if (!match.Success)
                {
                    validationResult.Errors.Add(new ValidationFailure("Invalid Line Data Pattern", line));
                    continue;
                }

                TransactionEntity transactionLine = ParseTransactionLine(match);
                transactionLines.Add(transactionLine);
            }

            if(!validationResult.Errors.Any() && transactionLines.Any())
            {
                List<TransactionModel> transactions = transactionLines.Select(domain => 
                new TransactionModel(
                    Guid.NewGuid(),
                    (TransactionTypeModel)domain.Type,
                    domain.Date,
                    domain.Product,
                    domain.Amount,
                    domain.Seller
                    )
                ).ToList();
                _transactionRepository.AddRange(transactions);
                _transactionRepository.SaveChanges();

                return true;
            }
        }

        var errors = validationResult.Errors.ConvertAll(failure => Error.Validation(failure.PropertyName, failure.ErrorMessage));
        return errors;
    }

    //@@@ manipular a lista de erros caso de erro em algum parse
    private static TransactionEntity ParseTransactionLine(Match match)
    {
        // Parse the matched values from the regular expression groups
        TransactionType type = (TransactionType)int.Parse(match.Groups[1].Value);
        DateTime date = DateTime.Parse(match.Groups[2].Value);
        string product = match.Groups[3].Value.Trim();
        decimal amount = int.Parse(match.Groups[4].Value);
        string seller = match.Groups[5].Value.Trim();

        // Create the Transaction domain model object
        TransactionEntity transaction = new(type, date, product, amount, seller);

        return transaction;
    }
}