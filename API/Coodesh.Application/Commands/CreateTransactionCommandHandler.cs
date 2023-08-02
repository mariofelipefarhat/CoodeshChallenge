
using Coodesh.Application.Interfaces;
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
    public CreateTransactionCommandHandler(ITransactionFileProcessorService fileProcessorService, IValidator<CreateTransactionCommand> validator,
        ITransactionRepository transactionRepository)
    {
        _fileProcessorService = fileProcessorService;
        _validator = validator;
        _transactionRepository = transactionRepository;
    }

    public CreateTransactionCommandHandler(ITransactionRepository transactionRepository, IValidator<CreateTransactionCommand> validator)
    {
        _validator = validator;
        _transactionRepository = transactionRepository;
    }

    public async Task<ErrorOr<bool>> Handle(CreateTransactionCommand cmd, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(cmd, cancellationToken);

        if (validationResult.IsValid)        
            return _fileProcessorService.ProcessFile(cmd.Stream.OpenReadStream());        

        return validationResult.Errors.ConvertAll(failure => Error.Validation(failure.PropertyName, failure.ErrorMessage));
    }
}