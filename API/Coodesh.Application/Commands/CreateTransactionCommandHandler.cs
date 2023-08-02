
using Coodesh.Application.Interfaces;
using Coodesh.Infrastructure.Models.Transaction;
using Coodesh.Infrastructure.Persistence.Transaction;
using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Coodesh.Application.Commands;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, ErrorOr<bool>>
{
    private readonly ITransactionFileProcessorService _fileProcessorService;
    private readonly IValidator<CreateTransactionCommand> _validator;
    private readonly ITransactionRepository _transactionRepository;

    public CreateTransactionCommandHandler(ITransactionRepository transactionRepository, IValidator<CreateTransactionCommand> validator,
        ITransactionFileProcessorService fileProcessorService)
    {
        _transactionRepository = transactionRepository;
        _validator = validator;
        _fileProcessorService = fileProcessorService;
    }

    public async Task<ErrorOr<bool>> Handle(CreateTransactionCommand cmd, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(cmd, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.Errors.ConvertAll(failure => Error.Validation(failure.PropertyName, failure.ErrorMessage));

        ErrorOr<List<TransactionModel>> processResult = _fileProcessorService.ProcessFile(cmd.Stream.OpenReadStream());

        if (processResult.IsError)
            return processResult.Errors;

        _transactionRepository.AddRange(processResult.Value);
        _transactionRepository.SaveChanges();

        return true;
    }
}