using FluentValidation;

namespace Coodesh.Application.Commands;

public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        RuleFor(file => file.Stream)
            .NotNull()
            .WithMessage("File not provided.");

        RuleFor(file => file.Stream.Length)
            .NotEmpty()
            .WithMessage("File is empty.");

        RuleFor(file => file.Stream.ContentType)
            .Must(ValidContentType)
            .WithMessage("Invalid file content type.");
    }

    private bool ValidContentType(string contentType)
    {
        return contentType.Equals("text/plain", StringComparison.OrdinalIgnoreCase);
    }
}
